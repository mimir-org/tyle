import { DevTool } from "@hookform/devtools";
import { Aspect } from "@mimirorg/typelibrary-types";
import { useForm, useWatch } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { Box } from "../../../complib/layouts";
import { useCreateNode, useUpdateNode } from "../../../data/queries/tyle/queriesNode";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { createEmptyFormNodeLib, FormNodeLib, mapFormNodeLibToApiModel } from "../types/formNodeLib";
import { useNodeSubmissionToast } from "./NodeForm.helpers";
import { NodeFormContainer } from "./NodeForm.styled";
import { NodeFormBaseFields } from "./NodeFormBaseFields";
import { FunctionNode, LocationNode, ProductNode } from "./variants";

interface NodeFormProps {
  defaultValues?: FormNodeLib;
  isEdit?: boolean;
}

export const NodeForm = ({ defaultValues = createEmptyFormNodeLib(), isEdit }: NodeFormProps) => {
  const theme = useTheme();
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormNodeLib>({ defaultValues });

  const nodeUpdateMutation = useUpdateNode();
  const nodeCreateMutation = useCreateNode();
  useNavigateOnCriteria("/", nodeCreateMutation.isSuccess || nodeUpdateMutation.isSuccess);

  const aspect = useWatch({ control, name: "aspect" });
  const toastNodeSubmission = useNodeSubmissionToast();

  const onSubmit = (data: FormNodeLib) => {
    const mutation = isEdit ? nodeUpdateMutation.mutateAsync : nodeCreateMutation.mutateAsync;
    const submittable = mapFormNodeLibToApiModel(data);
    const submissionPromise = mutation(submittable);
    toastNodeSubmission(submissionPromise);
    return submissionPromise;
  };

  return (
    <NodeFormContainer onSubmit={handleSubmit((data) => onSubmit(data))}>
      <NodeFormBaseFields
        control={control}
        register={register}
        reset={reset}
        resetField={resetField}
        setValue={setValue}
      />

      <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
        {aspect === Aspect.Function && <FunctionNode control={control} register={register} />}
        {aspect === Aspect.Location && <LocationNode control={control} register={register} />}
        {aspect === Aspect.Product && <ProductNode control={control} register={register} />}
      </Box>

      <DevTool control={control} placement={"bottom-right"} />
    </NodeFormContainer>
  );
};
