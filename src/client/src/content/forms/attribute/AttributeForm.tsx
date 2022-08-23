import { DevTool } from "@hookform/devtools";
import { useForm } from "react-hook-form";
import { useCreateAttribute } from "../../../data/queries/tyle/queriesAttribute";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { useAttributeSubmissionToast, usePrefilledAttributeData } from "./AttributeForm.helpers";
import { AttributeFormContainer } from "./AttributeForm.styled";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";
import { createEmptyFormAttributeLib, FormAttributeLib, mapFormAttributeLibToApiModel } from "./types/formAttributeLib";

interface AttributeFormProps {
  defaultValues?: FormAttributeLib;
  isEdit?: boolean;
}

export const AttributeForm = ({ defaultValues = createEmptyFormAttributeLib() }: AttributeFormProps) => {
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormAttributeLib>({ defaultValues });

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
        <AttributeFormBaseFields
          control={control}
          register={register}
          resetField={resetField}
          setValue={setValue}
          hasPrefilledData={hasPrefilledData}
        />
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </AttributeFormContainer>
  );
};
