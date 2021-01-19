using OpenTK.Graphics.OpenGL;
using AtominaCraft.ZResources.Logging;
using AtominaCraft.ZResources.Maths;
using AtominaCraft.ZResources.Strings;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.ZResources.Graphics
{
    public class Shader : IDisposable
    {
        public string ShaderName { get; private set; }

        /// <summary>
        /// ID to the location of the vertex shader
        /// </summary>
        public int VertexID { get; private set; }
        /// <summary>
        /// ID to the location of the fragment shader
        /// </summary>
        public int FragmentID { get; private set; }
        /// <summary>
        /// ID to the location of the shader program
        /// </summary>
        public int ProgramID { get; private set; }
        /// <summary>
        /// ID to the location of the model view matrix (local object space, for lighting and stuff)
        /// </summary>
        public int MVID { get; private set; }
        /// <summary>
        /// ID to the location of the model view projected matrix (projected world view, to move vertices around in the shader)
        /// </summary>
        public int MVPID { get; private set; }
        /// <summary>
        /// ID to the location of the uniform colour
        /// </summary>
        public int ColourID { get; private set; }

        /// <summary>
        /// Collection of shader attributes, like in/out variables
        /// </summary>
        public List<string> VariableBindingAttributes { get; private set; }

        public Shader(string vertexSource, string fragmentSource)
        {
            VariableBindingAttributes = new List<string>();

            VertexID = LoadShader(vertexSource, ShaderType.VertexShader);
            FragmentID = LoadShader(fragmentSource, ShaderType.FragmentShader);

            // Create shader program
            ProgramID = GL.CreateProgram();
            GL.AttachShader(ProgramID, VertexID);
            GL.AttachShader(ProgramID, FragmentID);

            GL.BindAttribLocation(ProgramID, 0, "mvp");

            for (int i = 0; i < VariableBindingAttributes.Count; i++)
            {
                string attribute = VariableBindingAttributes[(int)i];
                GL.BindAttribLocation(ProgramID, i, attribute);
                LogManager.GraphicsLogger.Log($"Linked attribute '{attribute}'");
            }

            GL.LinkProgram(ProgramID);

            GL.GetProgram(ProgramID, GetProgramParameterName.LinkStatus, out int isLinked);
            if (isLinked < 1)
            {
                // error
                LogManager.GraphicsLogger.Log($"Failed to link shader. Shader Name: {ShaderName}");
                GL.GetProgram(ProgramID, GetProgramParameterName.InfoLogLength, out int length);
                GL.GetProgramInfoLog(ProgramID, length, out length, out string log);
                LogManager.GraphicsLogger.Log("Error: ");
                LogManager.LogRawUntimed(log);
            }

            MVPID = GL.GetUniformLocation(ProgramID, "mvp");
            MVID = GL.GetUniformLocation(ProgramID, "mv");
            ColourID = GL.GetUniformLocation(ProgramID, "in_colour");
            //TextureID = GL.GetUniformLocation(ProgramID, "tex");

            LogUniformAvaliable("mvp", MVPID);
            LogUniformAvaliable("mv", MVID);
            LogUniformAvaliable("in_colour", ColourID);

            GL.DetachShader(ProgramID, VertexID);
            GL.DetachShader(ProgramID, FragmentID);
            GL.DeleteShader(VertexID);
            GL.DeleteShader(FragmentID);
        }

        private void LogUniformAvaliable(string uniformName, int uniformLocation)
        {
            if (uniformLocation < 0)
            {
                LogManager.GraphicsLogger.Log($"Uniform '{uniformName}' is invalid. Location: {uniformLocation}");
            }
            else
            {
                LogManager.GraphicsLogger.Log($"Uniform '{uniformName}' is valid. Location: {uniformLocation}");
            }
        }

        /// <summary>
        /// Loads a shader from the shaders directory
        /// </summary>
        /// <returns>the ID of the shader created</returns>
        private int LoadShader(string sources, ShaderType type)
        {
            int shaderID = GL.CreateShader(type);
            GL.ShaderSource(shaderID, sources);
            GL.CompileShader(shaderID);

            // Error checking
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out int isCompiled);
            if (isCompiled == 0)
            {
                string shaderType = "non vertex or fragment";
                if (type == ShaderType.VertexShader) shaderType = "vertex";
                if (type == ShaderType.FragmentShader) shaderType = "fragment";

                LogManager.GraphicsLogger.Log($"Failed to compile {shaderType} shader. Shader Name: {ShaderName}");
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
            {
                foreach (string line in string.Join('\n', sources).Split('\n'))
                {
                    try
                    {
                        if (line.Trim().Check(0, 2, "in"))
                        {
                            int startAttrib = 3;
                            int endAttrib = line.IndexOf(";", startAttrib);
                            if (endAttrib != -1)
                            {
                                string fullVariable = line.Extract(startAttrib, endAttrib - startAttrib).TrimStart().TrimEnd();
                                int afterVariableType = fullVariable.IndexOf(" ") + 1;
                                string variableName = fullVariable.Extract(afterVariableType);
                                VariableBindingAttributes.Add(variableName.Trim());
                            }
                        }
                    }
                    catch { }
                }
            }

            return shaderID;
        }

        public void Use()
        {
            GL.UseProgram(ProgramID);
        }

        public void SetMatrix(Matrix4 mvp, Matrix4 mv)
        {
            if (MVPID >= 0 && mvp != null)
            {
                GL.UniformMatrix4(MVPID, 1, true, mvp.M);
            }
            if (MVID >= 0 && mv != null)
            {
                GL.UniformMatrix4(MVID, 1, true, mv.M);
            }
        }

        public void SetColourShader(float r, float g, float b)
        {
            if (ColourID >= 0)
            {
                GL.Uniform3(ColourID, r, g, b);
            }
        }

        public void Dispose()
        {
            GL.DeleteProgram(ProgramID);
        }
    }
}