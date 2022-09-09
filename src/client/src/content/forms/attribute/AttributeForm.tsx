import { DevTool } from "@hookform/devtools";
import { Control, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useCreateAttribute, useGetAttributesReference } from "../../../data/queries/tyle/queriesAttribute";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { FormReferences, HasReferences } from "../common/FormReferences";
import { onSubmitForm } from "../common/onSubmitForm";
import { useSubmissionToast } from "../common/useSubmissionToast";
import { showSelectValues, usePrefilledAttributeData } from "./AttributeForm.helpers";
import { AttributeFormContainer } from "./AttributeForm.styled";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";
import { createEmptyFormAttributeLib, FormAttributeLib, mapFormAttributeLibToApiModel } from "./types/formAttributeLib";
import { AttributeFormUnits } from "./units/AttributeFormUnits";
import { AttributeFormValues } from "./values/AttributeFormValues";

interface AttributeFormProps {
  defaultValues?: FormAttributeLib;
  isEdit?: boolean;
}

export const AttributeForm = ({ defaultValues = createEmptyFormAttributeLib() }: AttributeFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { register, handleSubmit, control, reset, resetField } = useForm<FormAttributeLib>({ defaultValues });
  const attributeSelect = useWatch({ control, name: "select" });

  const [hasPrefilledData, isLoading] = usePrefilledAttributeData(reset);

  const attributeCreateMutation = useCreateAttribute();
  const attributeReferences = useGetAttributesReference();

  const toast = useSubmissionToast(t("attribute.title"));

  useNavigateOnCriteria("/", attributeCreateMutation.isSuccess);

  return (
    <AttributeFormContainer
      onSubmit={handleSubmit((data) =>
        onSubmitForm(mapFormAttributeLibToApiModel(data), attributeCreateMutation.mutateAsync, toast)
      )}
    >
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <AttributeFormBaseFields
            control={control}
            register={register}
            resetField={resetField}
            hasPrefilledData={hasPrefilledData}
          />

          <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
            <AttributeFormUnits register={register} control={control} />
            <FormReferences
              control={control as unknown as Control<HasReferences>}
              references={attributeReferences.data ?? []}
              isLoading={attributeReferences.isLoading}
            />
            {showSelectValues(attributeSelect) && <AttributeFormValues control={control} />}
          </Box>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </AttributeFormContainer>
  );
};
