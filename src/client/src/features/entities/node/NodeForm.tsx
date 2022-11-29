import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { NodeLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box } from "complib/layouts";
import { Loader } from "features/common/loader";
import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { getSubformForAspect, useNodeMutation, useNodeQuery } from "features/entities/node/NodeForm.helpers";
import { NodeFormContainer } from "features/entities/node/NodeForm.styled";
import { NodeFormBaseFields } from "features/entities/node/NodeFormBaseFields";
import { nodeSchema } from "features/entities/node/nodeSchema";
import {
  createEmptyFormNodeLib,
  FormNodeLib,
  mapFormNodeLibToApiModel,
  mapNodeLibCmToClientModel,
} from "features/entities/node/types/formNodeLib";
import { NodeFormMode } from "features/entities/node/types/nodeFormMode";
import { FormProvider, useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";

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
  const mapper = (source: NodeLibCm) => mapNodeLibCmToClientModel(source, mode);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

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
            <NodeFormBaseFields mode={mode} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
              {getSubformForAspect(aspect, mode)}
              <FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
                canRemoveAttributes={mode !== "edit"}
              />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </NodeFormContainer>
    </FormProvider>
  );
};
