import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useCreateAspectObject } from "external/sources/aspectobject/aspectObject.queries";
import {
  createEmptyFormAspectObjectLib,
  FormAspectObjectLib,
  mapFormAspectObjectLibToApiModel,
} from "features/entities/aspectobject/types/formAspectObjectLib";
import { useForm } from "react-hook-form";

interface Props {
  defaultValues?: FormAspectObjectLib;
}

/**
 * This form is only partially implemented and serves only as an example on how to utilize react hook form
 *
 * @param defaultValues
 * @constructor
 */
export const CreateOrEditAspectObjectFormWithMutation = ({ defaultValues = createEmptyFormAspectObjectLib() }: Props) => {
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm<FormAspectObjectLib>({ defaultValues });
  const mutation = useCreateAspectObject();
  useServerValidation(mutation.error, setError);

  return (
    <div>
      <h1>Aspect object</h1>
      <span>Mutation status: </span>
      <span>
        {mutation.isLoading && "Creating qualifier 🔄"}
        {mutation.isSuccess && "Qualifier created ✅"}
        {mutation.isError && "Qualifier not created ❌"}
      </span>

      <h2>Submission form</h2>
      <form
        style={{ display: "flex", flexDirection: "column", gap: "10px", width: "500px", maxWidth: "500px" }}
        onSubmit={handleSubmit((data) => mutation.mutate(mapFormAspectObjectLibToApiModel(data)))}
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
