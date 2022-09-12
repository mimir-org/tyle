import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Box } from "../../../complib/layouts";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { useServerValidation } from "../../../hooks/useServerValidation";
import { Loader } from "../../common/loader";
import { FormAttributes } from "../common/form-attributes/FormAttributes";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { prepareAttributesByAspect } from "../common/utils/prepareAttributesByAspect";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { getFormForAspect, useNodeMutation, useNodeQuery } from "./NodeForm.helpers";
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
  const { register, handleSubmit, control, setValue, setError, reset, resetField, formState } = useForm<FormNodeLib>({
    defaultValues,
  });

  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const query = useNodeQuery();
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapNodeLibCmToFormNodeLib, reset);

  const toast = useSubmissionToast(t("node.title"));

  const mutation = useNodeMutation(isEdit);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  return (
    <NodeFormContainer
      onSubmit={handleSubmit((data) => onSubmitForm(mapFormNodeLibToApiModel(data), mutation.mutateAsync, toast))}
    >
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <NodeFormBaseFields
            control={control}
            register={register}
            resetField={resetField}
            setValue={setValue}
            errors={formState.errors}
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
