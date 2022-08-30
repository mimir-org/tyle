import { DevTool } from "@hookform/devtools";
import { useForm, useWatch } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { Box } from "../../../complib/layouts";
import { useCreateNode, useUpdateNode } from "../../../data/queries/tyle/queriesNode";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { getFormForAspect, useNodeSubmissionToast, usePrefilledNodeData } from "./NodeForm.helpers";
import { NodeFormContainer } from "./NodeForm.styled";
import { NodeFormBaseFields } from "./NodeFormBaseFields";
import { createEmptyFormNodeLib, FormNodeLib, mapFormNodeLibToApiModel } from "./types/formNodeLib";

interface NodeFormProps {
  defaultValues?: FormNodeLib;
  isEdit?: boolean;
}

export const NodeForm = ({ defaultValues = createEmptyFormNodeLib(), isEdit }: NodeFormProps) => {
  const theme = useTheme();
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormNodeLib>({ defaultValues });
  const aspect = useWatch({ control, name: "aspect" });

  const nodeUpdateMutation = useUpdateNode();
  const nodeCreateMutation = useCreateNode();
  const [hasPrefilledData, isLoading] = usePrefilledNodeData(reset);

  const toastNodeSubmission = useNodeSubmissionToast();
  const onSubmit = (data: FormNodeLib) => {
    const mutation = isEdit ? nodeUpdateMutation.mutateAsync : nodeCreateMutation.mutateAsync;
    const submittable = mapFormNodeLibToApiModel(data);
    const submissionPromise = mutation(submittable);
    toastNodeSubmission(submissionPromise);
    return submissionPromise;
  };

  useNavigateOnCriteria("/", nodeCreateMutation.isSuccess || nodeUpdateMutation.isSuccess);

  return (
    <NodeFormContainer onSubmit={handleSubmit((data) => onSubmit(data))}>
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <NodeFormBaseFields
            control={control}
            register={register}
            resetField={resetField}
            setValue={setValue}
            hasPrefilledData={hasPrefilledData}
          />

          <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
            {getFormForAspect(aspect, control, register)}
          </Box>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </NodeFormContainer>
  );
};
