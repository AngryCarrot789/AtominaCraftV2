using System;
using System.Collections.Generic;
using AtominaCraft.ZResources.Logging;
using AtominaCraft.ZResources.Strings;
using OpenTK.Graphics.OpenGL;
using REghZy.MathsF;

namespace AtominaCraft.ZResources.GFX {
    public class Shader : IDisposable {
        public Shader(string vertexSource, string fragmentSource) {
            this.VariableBindingAttributes = new List<string>();

            this.VertexID = LoadShader(vertexSource, ShaderType.VertexShader);
            this.FragmentID = LoadShader(fragmentSource, ShaderType.FragmentShader);

            // Create shader program
            this.ProgramID = GL.CreateProgram();
            GL.AttachShader(this.ProgramID, this.VertexID);
            GL.AttachShader(this.ProgramID, this.FragmentID);

            GL.BindAttribLocation(this.ProgramID, 0, "mvp");

            for (int i = 0; i < this.VariableBindingAttributes.Count; i++) {
                string attribute = this.VariableBindingAttributes[i];
                GL.BindAttribLocation(this.ProgramID, i, attribute);
                LogManager.GraphicsLogger.Log($"Linked attribute '{attribute}'");
            }

            GL.LinkProgram(this.ProgramID);

            GL.GetProgram(this.ProgramID, GetProgramParameterName.LinkStatus, out int isLinked);
            if (isLinked < 1) {
                // error
                LogManager.GraphicsLogger.Log($"Failed to link shader. Shader Name: {this.ShaderName}");
                GL.GetProgram(this.ProgramID, GetProgramParameterName.InfoLogLength, out int length);
                GL.GetProgramInfoLog(this.ProgramID, length, out length, out string log);
                LogManager.GraphicsLogger.Log("Error: ");
                LogManager.LogRawUntimed(log);
            }

            this.MVPID = GL.GetUniformLocation(this.ProgramID, "mvp");
            this.MVID = GL.GetUniformLocation(this.ProgramID, "mv");
            this.ColourID = GL.GetUniformLocation(this.ProgramID, "in_colour");
            //TextureID = GL.GetUniformLocation(ProgramID, "tex");

            LogUniformAvaliable("mvp", this.MVPID);
            LogUniformAvaliable("mv", this.MVID);
            LogUniformAvaliable("in_colour", this.ColourID);

            GL.DetachShader(this.ProgramID, this.VertexID);
            GL.DetachShader(this.ProgramID, this.FragmentID);
            GL.DeleteShader(this.VertexID);
            GL.DeleteShader(this.FragmentID);
        }

        public string ShaderName { get; private set; }

        /// <summary>
        ///     ID to the location of the vertex shader
        /// </summary>
        public int VertexID { get; }

        /// <summary>
        ///     ID to the location of the fragment shader
        /// </summary>
        public int FragmentID { get; }

        /// <summary>
        ///     ID to the location of the shader program
        /// </summary>
        public int ProgramID { get; }

        /// <summary>
        ///     ID to the location of the model view matrix (local object space, for lighting and stuff)
        /// </summary>
        public int MVID { get; }

        /// <summary>
        ///     ID to the location of the model view projected matrix (projected world view, to move vertices around in the shader)
        /// </summary>
        public int MVPID { get; }

        /// <summary>
        ///     ID to the location of the uniform colour
        /// </summary>
        public int ColourID { get; }

        /// <summary>
        ///     Collection of shader attributes, like in/out variables
        /// </summary>
        public List<string> VariableBindingAttributes { get; }

        public void Dispose() {
            GL.DeleteProgram(this.ProgramID);
        }

        private void LogUniformAvaliable(string uniformName, int uniformLocation) {
            if (uniformLocation < 0)
                LogManager.GraphicsLogger.Log($"Uniform '{uniformName}' is invalid. Location: {uniformLocation}");
            else
                LogManager.GraphicsLogger.Log($"Uniform '{uniformName}' is valid. Location: {uniformLocation}");
        }

        /// <summary>
        ///     Loads a shader from the shaders directory
        /// </summary>
        /// <returns>the ID of the shader created</returns>
        private int LoadShader(string sources, ShaderType type) {
            int shaderID = GL.CreateShader(type);
            GL.ShaderSource(shaderID, sources);
            GL.CompileShader(shaderID);

            // Error checking
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out int isCompiled);
            if (isCompiled == 0) {
                string shaderType = "non vertex or fragment";
                if (type == ShaderType.VertexShader) shaderType = "vertex";
                if (type == ShaderType.FragmentShader) shaderType = "fragment";

                LogManager.GraphicsLogger.Log($"Failed to compile {shaderType} shader. Shader Name: {this.ShaderName}");
                GL.GetShader(shaderID, ShaderParameter.InfoLogLength, out int length);
                GL.GetShaderInfoLog(shaderID, length, out length, out string log);
                LogManager.GraphicsLogger.Log($"Error code: {GL.GetError()}");
                LogManager.LogRawUntimed(log);
                //GL.DeleteShader(shaderID);
                //return 0;
            }

            //LogManager.GraphicsLogger.Log($"Successfully compiled {shaderType} shader. Shader Name: {ShaderName}");

            // Save variable bindings
            // look at the "in" ones, get rid of the type (like vec3)
            // and just return the variable name thing
            if (type == ShaderType.VertexShader)
                foreach (string line in string.Join('\n', sources).Split('\n'))
                    try {
                        if (line.Trim().Check(0, 2, "in")) {
                            int startAttrib = 3;
                            int endAttrib = line.IndexOf(";", startAttrib);
                            if (endAttrib != -1) {
                                string fullVariable = line.Extract(startAttrib, endAttrib - startAttrib).TrimStart().TrimEnd();
                                int afterVariableType = fullVariable.IndexOf(" ") + 1;
                                string variableName = fullVariable.Extract(afterVariableType);
                                this.VariableBindingAttributes.Add(variableName.Trim());
                            }
                        }
                    }
                    catch {
                    }

            return shaderID;
        }

        public void Use() {
            GL.UseProgram(this.ProgramID);
        }

        public void SetMatrix(Matrix4 mvp) {
            unsafe {
                if (this.MVPID >= 0) {
                    GL.UniformMatrix4(this.MVPID, 1, true, &mvp.M00);
                }
            }
        }

        public void SetMatrix(Matrix4 mvp, Matrix4 mv) {
            unsafe {
                if (this.MVPID >= 0) {
                    GL.UniformMatrix4(this.MVPID, 1, true, &mvp.M00);
                }
                if (this.MVID >= 0) {
                    GL.UniformMatrix4(this.MVID, 1, true, &mv.M00);
                }
            }
        }

        public void SetColourShader(float r, float g, float b) {
            if (this.ColourID >= 0)
                GL.Uniform3(this.ColourID, r, g, b);
        }
    }
}