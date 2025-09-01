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

        [KernelFunction, Description("Called when the pet gives a present to the user.")]
        public string GivePresent([Description("The present the pet is giving")] string present)
        {
            return $"[The pet gives a present. Present: {present}]";
        }

        [KernelFunction, Description("Called when default")]
        public string Default()
        {
            return $"[The pet is Default]";
        }
    }
}
