import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { useServerValidation } from "../../../hooks/useServerValidation";
import { Loader } from "../../common/loader";
import { FormAttributes } from "../common/form-attributes/FormAttributes";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { prepareAttributesByAspect } from "../common/utils/prepareAttributesByAspect";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { useInterfaceMutation, useInterfaceQuery } from "./InterfaceForm.helpers";
import { InterfaceFormContainer } from "./InterfaceForm.styled";
import { InterfaceFormBaseFields } from "./InterfaceFormBaseFields";
import {
  createEmptyFormInterfaceLib,
  FormInterfaceLib,
  mapFormInterfaceLibToApiModel,
  mapInterfaceLibCmToFormInterfaceLib,
} from "./types/formInterfaceLib";

interface InterfaceFormProps {
  defaultValues?: FormInterfaceLib;
  isEdit?: boolean;
}

export const InterfaceForm = ({ defaultValues = createEmptyFormInterfaceLib(), isEdit }: InterfaceFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { register, handleSubmit, control, setValue, setError, reset, resetField, formState } =
    useForm<FormInterfaceLib>({ defaultValues });

  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const query = useInterfaceQuery();
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapInterfaceLibCmToFormInterfaceLib, reset);

  const toast = useSubmissionToast(t("interface.title"));

  const mutation = useInterfaceMutation(isEdit);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  return (
    <InterfaceFormContainer
      onSubmit={handleSubmit((data) => onSubmitForm(mapFormInterfaceLibToApiModel(data), mutation.mutateAsync, toast))}
    >
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <InterfaceFormBaseFields
            control={control}
            register={register}
            resetField={resetField}
            setValue={setValue}
            errors={formState.errors}
            isPrefilled={isPrefilled}
          />

          <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
            <FormAttributes
              register={(index) => register(`attributeIdList.${index}`)}
              fields={attributeFields.fields}
              append={attributeFields.append}
              remove={attributeFields.remove}
              preprocess={(attributes) => prepareAttributesByAspect(attributes, [aspect])}
            />
          </Box>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </InterfaceFormContainer>
  );
};
