import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { FormProvider, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useCreateAttribute, useGetAttributesReference } from "../../../data/queries/tyle/queriesAttribute";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { useServerValidation } from "../../../hooks/useServerValidation";
import { Loader } from "../../common/loader";
import { FormReferences } from "../common/form-references/FormReferences";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { attributeSchema, showSelectValues, useAttributeQuery } from "./AttributeForm.helpers";
import { AttributeFormContainer } from "./AttributeForm.styled";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";
import {
  createEmptyFormAttributeLib,
  FormAttributeLib,
  mapAttributeLibCmToFormAttributeLib,
  mapFormAttributeLibToApiModel,
} from "./types/formAttributeLib";
import { AttributeFormUnits } from "./units/AttributeFormUnits";
import { AttributeFormValues } from "./values/AttributeFormValues";

interface AttributeFormProps {
  defaultValues?: FormAttributeLib;
  isEdit?: boolean;
}

export const AttributeForm = ({ defaultValues = createEmptyFormAttributeLib() }: AttributeFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();

  const formMethods = useForm<FormAttributeLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(attributeSchema(t)),
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const attributeSelect = useWatch({ control, name: "select" });
  const attributeReferences = useGetAttributesReference();

  const query = useAttributeQuery();
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapAttributeLibCmToFormAttributeLib, reset);

  const mutation = useCreateAttribute();
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("attribute.title"));

  return (
    <FormProvider {...formMethods}>
      <AttributeFormContainer
        onSubmit={handleSubmit((data) =>
          onSubmitForm(mapFormAttributeLibToApiModel(data), mutation.mutateAsync, toast)
        )}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <AttributeFormBaseFields isPrefilled={isPrefilled} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
              <AttributeFormUnits />
              <FormReferences references={attributeReferences.data ?? []} isLoading={attributeReferences.isLoading} />
              {showSelectValues(attributeSelect) && <AttributeFormValues />}
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </AttributeFormContainer>
    </FormProvider>
  );
};
