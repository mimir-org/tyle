import { DevTool } from "@hookform/devtools";
import { Box, FormContainer } from "@mimirorg/component-library";
import { BlockView } from "common/types/blocks/blockView";
import { FormMode } from "common/types/formMode";
import { onSubmitForm, usePrefilledForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import FormAttributes from "../FormAttributes";
import FormClassifiers from "../FormClassifiers";
import Loader from "../Loader";
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

interface BlockFormProps {
  defaultValues?: BlockFormFields;
  mode?: FormMode;
}

const BlockForm = ({ defaultValues = createDefaultBlockFormFields(), mode }: BlockFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<BlockFormFields>({
    defaultValues: defaultValues,
    //resolver: yupResolver(blockSchema(t)),
  });

  // const user = useGetCurrentUser();

  const { handleSubmit, control, setError, reset } = formMethods;
  // const aspect = useWatch({ control, name: "aspect" });
  //const attributeFields = useFieldArray({ control, name: "attributes" });
  //const attributeGroupFields = useFieldArray({ control, name: "attributeGroups" });

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
              <FormTerminals />
            </Box>

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              <FormClassifiers />
            </Box>
            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
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
