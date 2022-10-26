import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { NodeLibCm } from "@mimirorg/typelibrary-types";
import { FormProvider, useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Box } from "../../../complib/layouts";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { useServerValidation } from "../../../hooks/server-validation/useServerValidation";
import { Loader } from "../../../common/components/loader";
import { FormAttributes } from "../common/form-attributes/FormAttributes";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { prepareAttributes } from "../common/utils/prepareAttributes";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { getSubformForAspect, useNodeMutation, useNodeQuery } from "./NodeForm.helpers";
import { NodeFormContainer } from "./NodeForm.styled";
import { NodeFormBaseFields } from "./NodeFormBaseFields";
import { nodeSchema } from "./nodeSchema";
import {
  createEmptyFormNodeLib,
  FormNodeLib,
  mapFormNodeLibToApiModel,
  mapNodeLibCmToFormNodeLib,
} from "./types/formNodeLib";
import { NodeFormMode } from "./types/nodeFormMode";

interface NodeFormProps {
  defaultValues?: FormNodeLib;
  mode?: NodeFormMode;
}

export const NodeForm = ({ defaultValues = createEmptyFormNodeLib(), mode }: NodeFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();

  const formMethods = useForm<FormNodeLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(nodeSchema(t)),
  });

  const { register, handleSubmit, control, setError, reset } = formMethods;
  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributes" });

  const query = useNodeQuery();
  const mapper = (source: NodeLibCm) => mapNodeLibCmToFormNodeLib(source, mode);
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useNodeMutation(mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("node.title"));

  return (
    <FormProvider {...formMethods}>
      <NodeFormContainer
        onSubmit={handleSubmit((data) => onSubmitForm(mapFormNodeLibToApiModel(data), mutation.mutateAsync, toast))}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <NodeFormBaseFields isPrefilled={isPrefilled} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
              {getSubformForAspect(aspect)}
              <FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
              />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </NodeFormContainer>
    </FormProvider>
  );
};
