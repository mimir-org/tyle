import {
  createEmptyFormAspectObjectLib,
  FormAspectObjectLib,
} from "features/entities/aspectobject/types/formAspectObjectLib";
import { SubmitHandler, useForm } from "react-hook-form";

interface Props {
  defaultValues?: FormAspectObjectLib;
}

/**
 * This form is only partially implemented and serves only as an example on how to utilize react hook form
 *
 * @param defaultValues
 * @constructor
 */
export const CreateOrEditAspectObjectForm = ({ defaultValues = createEmptyFormAspectObjectLib() }: Props) => {
  const { register, handleSubmit } = useForm<FormAspectObjectLib>({ defaultValues });
  const onSubmit: SubmitHandler<FormAspectObjectLib> = (data) => console.log(data);

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <p>Aspect object name</p>
      <input placeholder="enter name" {...register("name")} />
      <button type={"submit"}>Submit</button>
    </form>
  );
};
