// import { BlockFormFields } from "features/entities/block/BlockForm.helpers";
// import { SubmitHandler, useForm } from "react-hook-form";

// interface Props {
//   defaultValues?: BlockFormFields;
// }

// /**
//  * This form is only partially implemented and serves only as an example on how to utilize react hook form
//  *
//  * @param defaultValues
//  * @constructor
//  */
// export const CreateOrEditBlockForm = ({ defaultValues = createEmptyFormBlockLib() }: Props) => {
//   const { register, handleSubmit } = useForm<BlockFormFields>({ defaultValues });
//   const onSubmit: SubmitHandler<BlockFormFields> = (data) => console.log(data);

//   return (
//     <form onSubmit={handleSubmit(onSubmit)}>
//       <p>Block name</p>
//       <input placeholder="enter name" {...register("name")} />
//       <button type={"submit"}>Submit</button>
//     </form>
//   );
// };
