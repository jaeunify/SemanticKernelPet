using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace PetService.Plugins
{
    public class PetPlugin
    {
        [KernelFunction, Description("Called when the pet is angry.")]
        public string Angry([Description("The reason why the pet is angry")] string reason)
        {
            return $"[The pet is angry. Reason: {reason}]";
        }

        [KernelFunction, Description("Called when the pet is happy.")]
        public string Happy([Description("The reason why the pet is happy")] string reason)
        {
            return $"[The pet is happy. Reason: {reason}]";
        }

        [KernelFunction, Description("Called when the pet is sad.")]
        public string Sad([Description("The reason why the pet is sad")] string reason)
        {
            return $"[The pet is sad. Reason: {reason}]";
        }

        [KernelFunction, Description("Called when the pet gives a present to the user.")]
        public string GivePresent([Description("The present the pet is giving")] string present)
        {
            return $"[The pet gives a present. Present: {present}]";
        }

        [KernelFunction, Description("Called when the pet really mad and run away from home")]
        public string RunAway([Description("The reason why the pet is running away")] string reason)
        {
            return $"[The pet is running away from home. Reason: {reason}]";
        }

        [KernelFunction, Description("Called when default")]
        public string Default()
        {
            return $"[The pet is Default]";
        }
    }
}
