import { DevTool } from "@hookform/devtools";
//import { yupResolver } from "@hookform/resolvers/yup";
//import { BlockLibCm, MimirorgPermission, State } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { Box, FormContainer } from "@mimirorg/component-library";
import { Loader } from "components/Loader";
//import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm, usePrefilledForm, useSubmissionToast } from "helpers/form.helpers";
//import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import {
  BlockFormFields,
  createDefaultBlockFormFields,
  //getSubformForAspect,
  // createEmptyFormBlockLib,
  // getSubformForAspect,
  // toApiModel,
  toBlockFormFields,
  toBlockTypeRequest,
  useBlockMutation,
  useBlockQuery,
} from "components/BlockForm/BlockForm.helpers";
//import { BlockFormBaseFields } from "features/entities/block/BlockFormBaseFields";
//import { blockSchema } from "features/entities/block/blockSchema";

import {
  FormProvider,
  useForm,
  // useWatch
} from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { FormMode } from "../../common/types/formMode";
//import { useGetLatestApprovedBlock } from "external/sources/block/block.queries";
// import { useGetCurrentUser } from "external/sources/user/user.queries";
import { BlockView } from "common/types/blocks/blockView";
import { FormAttributes } from "../FormAttributes/FormAttributes";
import { BlockFormBaseFields } from "./BlockFormBaseFields";
import { FormClassifiers } from "../TerminalForm/TerminalFormClassifiers";
import { FormTerminals } from "./FormTerminals";
//import { FormAttributeGroups } from "../common/form-attributeGroup/FormAttributeGroups";

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