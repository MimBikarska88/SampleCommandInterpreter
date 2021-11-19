using System;
using System.Linq;
using System.Reflection;
using CommandPattern.Core.Contracts;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            var inputInfo = args.Split();
            string commandname = inputInfo[0] + "Command";
            string[] parameters = inputInfo.Skip(1).ToArray();
            string result = string.Empty;

            Type type = Assembly.GetCallingAssembly().GetTypes().Where(type => type.Name==commandname).FirstOrDefault();
            
            if(type==null)
            {
                throw new InvalidOperationException("Invalid command");
            }

            ICommand command = (ICommand) Activator.CreateInstance(type) as ICommand;
            result = command.Execute(parameters);
            return result;
        }
    }
}