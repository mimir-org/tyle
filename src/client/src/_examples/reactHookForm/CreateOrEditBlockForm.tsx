import { createEmptyFormBlockLib, FormBlockLib } from "features/entities/block/types/formBlockLib";
import { SubmitHandler, useForm } from "react-hook-form";

interface Props {
  defaultValues?: FormBlockLib;
}

/**
 * This form is only partially implemented and serves only as an example on how to utilize react hook form
 *
 * @param defaultValues
 * @constructor
 */
export const CreateOrEditBlockForm = ({ defaultValues = createEmptyFormBlockLib() }: Props) => {
  const { register, handleSubmit } = useForm<FormBlockLib>({ defaultValues });
  const onSubmit: SubmitHandler<FormBlockLib> = (data) => console.log(data);

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <p>Block name</p>
      <input placeholder="enter name" {...register("name")} />
      <button type={"submit"}>Submit</button>
    </form>
  );
};
