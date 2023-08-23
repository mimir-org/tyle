import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useCreateBlock } from "external/sources/block/block.queries";
import {
  createEmptyFormBlockLib,
  FormBlockLib,
  mapFormBlockLibToApiModel,
} from "features/entities/block/types/formBlockLib";
import { useForm } from "react-hook-form";

interface Props {
  defaultValues?: FormBlockLib;
}

/**
 * This form is only partially implemented and serves only as an example on how to utilize react hook form
 *
 * @param defaultValues
 * @constructor
 */
export const CreateOrEditBlockFormWithMutation = ({ defaultValues = createEmptyFormBlockLib() }: Props) => {
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm<FormBlockLib>({ defaultValues });
  const mutation = useCreateBlock();
  useServerValidation(mutation.error, setError);

  return (
    <div>
      <h1>Block</h1>
      <span>Mutation status: </span>
      <span>
        {mutation.isLoading && "Creating qualifier 🔄"}
        {mutation.isSuccess && "Qualifier created ✅"}
        {mutation.isError && "Qualifier not created ❌"}
      </span>

      <h2>Submission form</h2>
      <form
        style={{ display: "flex", flexDirection: "column", gap: "10px", width: "500px", maxWidth: "500px" }}
        onSubmit={handleSubmit((data) => mutation.mutate(mapFormBlockLibToApiModel(data)))}
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
