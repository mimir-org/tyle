import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { Box, FormContainer } from "@mimirorg/component-library";
import FormAttributes from "components/FormAttributes";
import FormClassifiers from "components/FormClassifiers";
import Loader from "components/Loader";
import { onSubmitForm, usePrefilledForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { BlockView } from "types/blocks/blockView";
import { FormMode } from "types/formMode";
import {
  BlockFormFields,
  createDefaultBlockFormFields,
  toBlockFormFields,
  toBlockTypeRequest,
  useBlockMutation,
  useBlockQuery,
} from "./BlockForm.helpers";
import BlockFormBaseFields from "./BlockFormBaseFields";
import FormTerminals from "./FormTerminals";
import { blockSchema } from "./blockSchema";

interface BlockFormProps {
  defaultValues?: BlockFormFields;
  mode?: FormMode;
}

const BlockForm = ({ defaultValues = createDefaultBlockFormFields(), mode }: BlockFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<BlockFormFields>({
    defaultValues: defaultValues,
    resolver: yupResolver(blockSchema(t)),
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useBlockQuery();
  const mapper = (source: BlockView) => toBlockFormFields(source);

  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useBlockMutation(query.data?.id, mode);

  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("block.title"));

  const limited = false;

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) => onSubmitForm(toBlockTypeRequest(data), mutation.mutateAsync, toast))}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <BlockFormBaseFields limited={limited} mode={mode} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              <FormClassifiers />
            </Box>
            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              <FormTerminals />
              <FormAttributes />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};

export default BlockForm;
