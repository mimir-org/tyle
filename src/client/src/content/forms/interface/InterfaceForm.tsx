import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { FormAttributes } from "../common/FormAttributes";
import { onSubmitForm } from "../common/onSubmitForm";
import { prepareAttributesByAspect } from "../common/prepareAttributesByAspect";
import { usePrefilledForm } from "../common/usePrefilledForm";
import { useSubmissionToast } from "../common/useSubmissionToast";
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
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormInterfaceLib>({ defaultValues });

  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const query = useInterfaceQuery();
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapInterfaceLibCmToFormInterfaceLib, reset);

  const toast = useSubmissionToast(t("interface.title"));

  const mutation = useInterfaceMutation(isEdit);
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
