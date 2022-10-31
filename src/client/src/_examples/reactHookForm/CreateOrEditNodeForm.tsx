import { createEmptyFormNodeLib, FormNodeLib } from "features/entities/node/types/formNodeLib";
import { SubmitHandler, useForm } from "react-hook-form";

interface Props {
  defaultValues?: FormNodeLib;
}

/**
 * This form is only partially implemented and serves only as an example on how to utilize react hook form
 *
 * @param defaultValues
 * @constructor
 */
export const CreateOrEditNodeForm = ({ defaultValues = createEmptyFormNodeLib() }: Props) => {
  const { register, handleSubmit } = useForm<FormNodeLib>({ defaultValues });
  const onSubmit: SubmitHandler<FormNodeLib> = (data) => console.log(data);

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <p>Node name</p>
      <input placeholder="enter name" {...register("name")} />
      <button type={"submit"}>Submit</button>
    </form>
  );
};
