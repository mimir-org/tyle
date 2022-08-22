import { DevTool } from "@hookform/devtools";
import { useForm } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { AttributeFormContainer } from "./AttributeForm.styled";
import { FormAttributeLib } from "./types/formAttributeLib";
import { Loader } from "../../common/Loader";
import { Box } from "../../../complib/layouts";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";

interface AttributeFormProps {
  defaultValues?: FormAttributeLib;
  isEdit?: boolean;
}

export const AttributeForm = ({ defaultValues, isEdit }: AttributeFormProps) => {
  const theme = useTheme();
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormAttributeLib>({ defaultValues });

  const onSubmit = (data: FormAttributeLib) => {
    // const mutation = isEdit ? nodeUpdateMutation.mutateAsync : nodeCreateMutation.mutateAsync;
    // const submittable = mapFormNodeLibToApiModel(data);
    // const submissionPromise = mutation(submittable);
    // toastNodeSubmission(submissionPromise);
    return;
  };

  return (
    <AttributeFormContainer onSubmit={handleSubmit((data) => onSubmit(data))}>
      {/* {isLoading && <Loader />}
      {!isLoading && ( */}
      <>
        <AttributeFormBaseFields
          control={control}
          register={register}
          resetField={resetField}
          setValue={setValue}
          hasPrefilledData={false}
        />

        <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
          {/* {getFormForAspect(aspect, control, register)} */}
        </Box>
      </>
      {/* )} */}
      <DevTool control={control} placement={"bottom-right"} />
    </AttributeFormContainer>
  );
};
