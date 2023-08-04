import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { AspectObjectLibCm, MimirorgPermission, State } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box } from "complib/layouts";
import { Loader } from "features/common/loader";
import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import {
  getSubformForAspect,
  useAspectObjectMutation,
  useAspectObjectQuery,
} from "features/entities/aspectobject/AspectObjectForm.helpers";
import { AspectObjectFormBaseFields } from "features/entities/aspectobject/AspectObjectFormBaseFields";
import { aspectObjectSchema } from "features/entities/aspectobject/aspectObjectSchema";
import {
  createEmptyFormAspectObjectLib,
  FormAspectObjectLib,
  mapFormAspectObjectLibToApiModel,
  mapAspectObjectLibCmToClientModel,
} from "features/entities/aspectobject/types/formAspectObjectLib";
import { FormProvider, useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { FormContainer } from "../../../complib/form/FormContainer.styled";
import { FormMode } from "../types/formMode";
import { useGetLatestApprovedAspectObject } from "external/sources/aspectobject/aspectObject.queries";
import { useGetCurrentUser } from "external/sources/user/user.queries";

interface AspectObjectFormProps {
  defaultValues?: FormAspectObjectLib;
  mode?: FormMode;
}

export const AspectObjectForm = ({ defaultValues = createEmptyFormAspectObjectLib(), mode }: AspectObjectFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<FormAspectObjectLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(aspectObjectSchema(t)),
  });

  const user = useGetCurrentUser();

  const { register, handleSubmit, control, setError, reset } = formMethods;
  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributes" });

  const query = useAspectObjectQuery();
  const mapper = (source: AspectObjectLibCm) => {
    if (mode === "clone" && query.data?.companyId && user.data) {
      const permissionForCompany = user.data.permissions[query.data.companyId];
      if (!permissionForCompany || permissionForCompany < MimirorgPermission.Write) {
        const writeCompanies = Object.keys(user.data.permissions)
          .map((x) => Number(x))
          .filter((x) => x !== 0 && user.data.permissions[x] >= MimirorgPermission.Write);
        return mapAspectObjectLibCmToClientModel(source, writeCompanies[0]);
      }
    }
    return mapAspectObjectLibCmToClientModel(source);
  };
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useAspectObjectMutation(query.data?.id, mode);

  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("aspectObject.title"));

  const isFirstDraft =
    mode !== "edit" || (query.data?.state === State.Draft && query.data?.id === query.data?.firstVersionId);
  const limited = mode === "edit" && (query.data?.state === State.Approved || !isFirstDraft);

  const latestApprovedQuery = useGetLatestApprovedAspectObject(query.data?.id, limited);

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) =>
          onSubmitForm(mapFormAspectObjectLibToApiModel(data), mutation.mutateAsync, toast),
        )}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <AspectObjectFormBaseFields isFirstDraft={isFirstDraft} mode={mode} state={query.data?.state} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
              {getSubformForAspect(aspect, limited ? latestApprovedQuery.data?.aspectObjectTerminals : [])}
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
