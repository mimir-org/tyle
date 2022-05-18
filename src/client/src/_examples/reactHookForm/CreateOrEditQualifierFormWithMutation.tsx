import { useForm } from "react-hook-form";
import { useValidationFromServer } from "../../hooks/useValidationFromServer";
import { useCreateAttributeQualifier } from "../../data/queries/tyle/queriesAttributeQualifier";
import { getValidationStateFromServer } from "../../data/helpers/getValidationStateFromServer";
import {
  AttributeQualifierLibAm,
  createEmptyAttributeQualifierLibAm,
} from "../../models/tyle/application/attributeQualifierLibAm";

interface Props {
  defaultValues?: AttributeQualifierLibAm;
}

export const CreateOrEditQualifierFormWithMutation = ({
  defaultValues = createEmptyAttributeQualifierLibAm(),
}: Props) => {
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm<AttributeQualifierLibAm>({ defaultValues });
  const mutation = useCreateAttributeQualifier();
  const validationState = getValidationStateFromServer<AttributeQualifierLibAm>(mutation.error);
  useValidationFromServer<AttributeQualifierLibAm>(setError, validationState?.errors);

  return (
    <div>
      <h1>Attribute Qualifier</h1>
      <span>Mutation status: </span>
      <span>
        {mutation.isLoading && "Creating qualifier 🔄"}
        {mutation.isSuccess && "Qualifier created ✅"}
        {mutation.isError && "Qualifier not created ❌"}
      </span>

      <h2>Submission form</h2>
      <form
        style={{ display: "flex", flexDirection: "column", gap: "10px", width: "500px", maxWidth: "500px" }}
        onSubmit={handleSubmit((data) => mutation.mutate(data))}
      >
        <input placeholder="enter name" {...register("name")} />
        <p>Error field: {errors.name && errors.name.message}</p>

        <input placeholder="enter description" {...register("description")} />
        <p>Error field: {errors.description && errors.description.message}</p>

        <button>Submit</button>
      </form>
    </div>
  );
};
