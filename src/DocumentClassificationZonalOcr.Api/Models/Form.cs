using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public class Form : BaseEntity
    {
        public string Name { get; private set; }
        public List<FormSample> Samples { get; private set; }
        public List<Field> Fields { get; private set; }

        private Form() { }

        private Form(string name)
        {
            Name = name;
            Samples = new List<FormSample>();
            Fields = new List<Field>();
        }

        public static Result<Form> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Form>("Forms.Create", "Form name is required.");

            var form = new Form(name);
            return Result.Success(form);
        }

        public Result<bool> ModifyName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<bool>("Forms.ModifyName", "Form name cannot be null or empty.");

            Name = name;
            return Result.Success(true);
        }

        public Result<bool> AddSample(FormSample sample)
        {

            if (sample == null)
                return Result.Failure<bool>("Forms.AddSample", "Sample cannot be null.");

            if (Samples == null)
                Samples = new List<FormSample> { sample };
            Samples.Add(sample);
            return Result.Success(true);
        }

        public Result<bool> RemoveSample(int sampleId)
        {
            var sampleToRemove = Samples.FirstOrDefault(s => s.Id == sampleId);
            if (sampleToRemove == null)
                return Result.Failure<bool>("Forms.RemoveSample", "Sample not found.");

            Samples.Remove(sampleToRemove);
            return Result.Success(true);
        }

        public Result<bool> AddField(Field field)
        {
            if (field == null)
                return Result.Failure<bool>("Forms.AddField", "Field cannot be null.");

            if (Fields == null)
                Fields = new List<Field> { field };
            Fields.Add(field);
            return Result.Success(true);
        }

        public Result<bool> RemoveField(int fieldId)
        {
            var fieldToRemove = Fields.FirstOrDefault(f => f.Id == fieldId);
            if (fieldToRemove == null)
                return Result.Failure<bool>("Forms.RemoveField", "Field not found.");

            Fields.Remove(fieldToRemove);
            return Result.Success(true);
        }
    }
}
