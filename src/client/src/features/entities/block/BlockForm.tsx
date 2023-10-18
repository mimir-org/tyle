import { DevTool } from "@hookform/devtools";
//import { yupResolver } from "@hookform/resolvers/yup";
import { BlockLibCm, MimirorgPermission, State } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box, FormContainer } from "@mimirorg/component-library";
import { Loader } from "features/common/loader";
//import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
//import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import {
  BlockFormFields,
  createEmptyFormBlockLib,
  getSubformForAspect,
  toApiModel,
  useBlockMutation,
  useBlockQuery,
} from "features/entities/block/BlockForm.helpers";
import { BlockFormBaseFields } from "features/entities/block/BlockFormBaseFields";
//import { blockSchema } from "features/entities/block/blockSchema";

import { FormProvider, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { FormMode } from "../types/formMode";
import { useGetLatestApprovedBlock } from "external/sources/block/block.queries";
import { useGetCurrentUser } from "external/sources/user/user.queries";
import { BlockView } from "common/types/blocks/blockView";
//import { FormAttributeGroups } from "../common/form-attributeGroup/FormAttributeGroups";

interface BlockFormProps {
  defaultValues?: BlockFormFields;
  mode?: FormMode;
}

export const BlockForm = ({ defaultValues = createEmptyFormBlockLib(), mode }: BlockFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<BlockFormFields>({
    defaultValues: defaultValues,
    //resolver: yupResolver(blockSchema(t)),
  });

  const user = useGetCurrentUser();

  const { handleSubmit, control, setError, reset } = formMethods;
  const aspect = useWatch({ control, name: "aspect" });
  //const attributeFields = useFieldArray({ control, name: "attributes" });
  //const attributeGroupFields = useFieldArray({ control, name: "attributeGroups" });

  const query = useBlockQuery();
  const mapper = (source: BlockView) => {
    if (mode === "clone" && query.data?.) {
      const permissionForCompany = user.data.permissions[query.data.companyId];
      if (!permissionForCompany || permissionForCompany < MimirorgPermission.Write) {
        const writeCompanies = Object.keys(user.data.permissions)
          .map((x) => Number(x))
          .filter((x) => x !== 0 && user.data.permissions[x] >= MimirorgPermission.Write);
        return mapBlockLibCmToClientModel(source, writeCompanies[0]);
      }
    }
    return mapBlockLibCmToClientModel(source);
  };
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useBlockMutation(query.data?.id, mode);

  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("block.title"));

  const isFirstDraft = query.data?.id === query.data;
  const limited = mode === "edit";

  const latestApprovedQuery = useGetLatestApprovedBlock(query.data?.id, limited);

  return (
    <FormProvider {...formMethods}>
      <FormContainer onSubmit={handleSubmit((data) => onSubmitForm(toApiModel(data), mutation.mutateAsync, toast))}>
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <BlockFormBaseFields isFirstDraft={isFirstDraft} mode={mode} state={query.data?.state} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              {getSubformForAspect(aspect, limited ? latestApprovedQuery.data?.blockTerminals : [])}
              {/*<FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
                limitedAttributes={limited ? latestApprovedQuery.data?.attributes : []}
              />
            </Box>

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              <FormAttributeGroups
                register={() => register(`attributeGroups`)}
                fields={attributeGroupFields.fields}
                append={attributeGroupFields.append}
                remove={attributeGroupFields.remove}
                limitedAttributeGroups={limited ? latestApprovedQuery.data?.attributes : []}
            />*/}
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
