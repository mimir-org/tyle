import { DevTool } from "@hookform/devtools";
import { useForm } from "react-hook-form";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useCreateAttribute } from "../../../data/queries/tyle/queriesAttribute";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { useAttributeSubmissionToast, usePrefilledAttributeData } from "./AttributeForm.helpers";
import { AttributeFormContainer } from "./AttributeForm.styled";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";
import { createEmptyFormAttributeLib, FormAttributeLib, mapFormAttributeLibToApiModel } from "./types/formAttributeLib";
import { AttributeFormUnits } from "./units/AttributeFormUnits";
import { AttributeFormValues } from "./values/AttributeFormValues";

interface AttributeFormProps {
  defaultValues?: FormAttributeLib;
  isEdit?: boolean;
}

export const AttributeForm = ({ defaultValues = createEmptyFormAttributeLib() }: AttributeFormProps) => {
  const theme = useTheme();
  const { register, handleSubmit, control, reset } = useForm<FormAttributeLib>({ defaultValues });

  const attributeCreateMutation = useCreateAttribute();
  const [hasPrefilledData, isLoading] = usePrefilledAttributeData(reset);

  const toastNodeSubmission = useAttributeSubmissionToast();
  const onSubmit = (data: FormAttributeLib) => {
    const submittable = mapFormAttributeLibToApiModel(data);
    const submissionPromise = attributeCreateMutation.mutateAsync(submittable);
    toastNodeSubmission(submissionPromise);
    return submissionPromise;
  };

  useNavigateOnCriteria("/", attributeCreateMutation.isSuccess);

  return (
    <AttributeFormContainer onSubmit={handleSubmit((data) => onSubmit(data))}>
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <AttributeFormBaseFields control={control} register={register} hasPrefilledData={hasPrefilledData} />

          <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
            <AttributeFormUnits register={register} control={control} />
            <AttributeFormValues control={control} />
          </Box>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </AttributeFormContainer>
  );
};
