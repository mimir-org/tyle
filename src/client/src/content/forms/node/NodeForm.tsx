import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Box } from "../../../complib/layouts";
import { useCreateNode, useUpdateNode } from "../../../data/queries/tyle/queriesNode";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { FormAttributes } from "../common/FormAttributes";
import { onSubmitForm } from "../common/onSubmitForm";
import { prepareAttributesByAspect } from "../common/prepareAttributesByAspect";
import { usePrefilledForm } from "../common/usePrefilledForm";
import { useSubmissionToast } from "../common/useSubmissionToast";
import { getFormForAspect, useNodeQuery } from "./NodeForm.helpers";
import { NodeFormContainer } from "./NodeForm.styled";
import { NodeFormBaseFields } from "./NodeFormBaseFields";
import {
  createEmptyFormNodeLib,
  FormNodeLib,
  mapFormNodeLibToApiModel,
  mapNodeLibCmToFormNodeLib,
} from "./types/formNodeLib";

interface NodeFormProps {
  defaultValues?: FormNodeLib;
  isEdit?: boolean;
}

export const NodeForm = ({ defaultValues = createEmptyFormNodeLib(), isEdit }: NodeFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormNodeLib>({ defaultValues });

  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const query = useNodeQuery();
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapNodeLibCmToFormNodeLib, reset);

  const nodeUpdateMutation = useUpdateNode();
  const nodeCreateMutation = useCreateNode();
  const targetMutation = isEdit ? nodeUpdateMutation : nodeCreateMutation;

  const toast = useSubmissionToast(t("node.title"));

  useNavigateOnCriteria("/", targetMutation.isSuccess);

  return (
    <NodeFormContainer
      onSubmit={handleSubmit((data) => onSubmitForm(mapFormNodeLibToApiModel(data), targetMutation.mutateAsync, toast))}
    >
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <NodeFormBaseFields
            control={control}
            register={register}
            resetField={resetField}
            setValue={setValue}
            isPrefilled={isPrefilled}
          />

          <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
            {getFormForAspect(aspect, control, register)}
            <FormAttributes
              register={(index) => register(`attributeIdList.${index}`)}
              fields={attributeFields.fields}
              append={attributeFields.append}
              remove={attributeFields.remove}
              preprocess={(attributes) => prepareAttributesByAspect(attributes, [aspect])}
            />
          </Box>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </NodeFormContainer>
  );
};
