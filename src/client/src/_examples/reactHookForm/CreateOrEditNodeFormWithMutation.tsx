import { useForm } from "react-hook-form";
import {
  createEmptyFormNodeLib,
  FormNodeLib,
  mapFormNodeLibToApiModel,
} from "../../features/entities/node/types/formNodeLib";
import { useCreateNode } from "../../data/queries/tyle/queriesNode";
import { useServerValidation } from "../../hooks/server-validation/useServerValidation";

interface Props {
  defaultValues?: FormNodeLib;
}

/**
 * This form is only partially implemented and serves only as an example on how to utilize react hook form
 *
 * @param defaultValues
 * @constructor
 */
export const CreateOrEditNodeFormWithMutation = ({ defaultValues = createEmptyFormNodeLib() }: Props) => {
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm<FormNodeLib>({ defaultValues });
  const mutation = useCreateNode();
  useServerValidation(mutation.error, setError);

  return (
    <div>
      <h1>Node</h1>
      <span>Mutation status: </span>
      <span>
        {mutation.isLoading && "Creating qualifier 🔄"}
        {mutation.isSuccess && "Qualifier created ✅"}
        {mutation.isError && "Qualifier not created ❌"}
      </span>

      <h2>Submission form</h2>
      <form
        style={{ display: "flex", flexDirection: "column", gap: "10px", width: "500px", maxWidth: "500px" }}
        onSubmit={handleSubmit((data) => mutation.mutate(mapFormNodeLibToApiModel(data)))}
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
