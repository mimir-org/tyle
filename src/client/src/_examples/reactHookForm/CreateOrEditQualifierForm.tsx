import { SubmitHandler, useForm } from "react-hook-form";
import {
  AttributeQualifierLibAm,
  createEmptyAttributeQualifierLibAm,
} from "../../models/typeLibrary/application/attributeQualifierLibAm";

interface Props {
  defaultValues?: AttributeQualifierLibAm;
}

export const CreateOrEditQualifierForm = ({ defaultValues = createEmptyAttributeQualifierLibAm() }: Props) => {
  const { register, handleSubmit } = useForm<AttributeQualifierLibAm>({ defaultValues });
  const onSubmit: SubmitHandler<AttributeQualifierLibAm> = (data) => console.log(data);

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <p>Attribute Qualifier</p>
      <input placeholder="enter name" {...register("name")} />
      <input placeholder="enter description" {...register("description")} />
      <button>Submit</button>
    </form>
  );
};
