import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { BlockLibCm, MimirorgPermission, State } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box, FormContainer } from "@mimirorg/component-library";
import { Loader } from "features/common/loader";
import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { getSubformForAspect, useBlockMutation, useBlockQuery } from "features/entities/block/BlockForm.helpers";
import { BlockFormBaseFields } from "features/entities/block/BlockFormBaseFields";
import { blockSchema } from "features/entities/block/blockSchema";
import {
  createEmptyFormBlockLib,
  FormBlockLib,
  mapFormBlockLibToApiModel,
  mapBlockLibCmToClientModel,
} from "features/entities/block/types/formBlockLib";
import { FormProvider, useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { FormMode } from "../types/formMode";
import { useGetLatestApprovedBlock } from "external/sources/block/block.queries";
import { useGetCurrentUser } from "external/sources/user/user.queries";

interface BlockFormProps {
  defaultValues?: FormBlockLib;
  mode?: FormMode;
}

export const BlockForm = ({ defaultValues = createEmptyFormBlockLib(), mode }: BlockFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<FormBlockLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(blockSchema(t)),
  });

  const user = useGetCurrentUser();

  const { register, handleSubmit, control, setError, reset } = formMethods;
  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributes" });

  const query = useBlockQuery();
  const mapper = (source: BlockLibCm) => {
    if (mode === "clone" && query.data?.companyId && user.data) {
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

  const isFirstDraft =
    mode !== "edit" || (query.data?.state === State.Draft && query.data?.id === query.data?.firstVersionId);
  const limited = mode === "edit" && (query.data?.state === State.Approved || !isFirstDraft);

  const latestApprovedQuery = useGetLatestApprovedBlock(query.data?.id, limited);

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) => onSubmitForm(mapFormBlockLibToApiModel(data), mutation.mutateAsync, toast))}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <BlockFormBaseFields isFirstDraft={isFirstDraft} mode={mode} state={query.data?.state} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              {getSubformForAspect(aspect, limited ? latestApprovedQuery.data?.blockTerminals : [])}
              <FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
                limitedAttributes={limited ? latestApprovedQuery.data?.attributes : []}
              />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
