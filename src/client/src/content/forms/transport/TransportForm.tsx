import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { FormAttributes } from "../common/form-attributes/FormAttributes";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { prepareAttributesByAspect } from "../common/utils/prepareAttributesByAspect";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { useTransportMutation, useTransportQuery } from "./TransportForm.helpers";
import { TransportFormContainer } from "./TransportForm.styled";
import { TransportFormBaseFields } from "./TransportFormBaseFields";
import {
  createEmptyFormTransportLib,
  FormTransportLib,
  mapFormTransportLibToApiModel,
  mapTransportLibCmToFormTransportLib,
} from "./types/formTransportLib";

interface TransportFormProps {
  defaultValues?: FormTransportLib;
  isEdit?: boolean;
}

export const TransportForm = ({ defaultValues = createEmptyFormTransportLib(), isEdit }: TransportFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormTransportLib>({ defaultValues });

  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const query = useTransportQuery();
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapTransportLibCmToFormTransportLib, reset);

  const toast = useSubmissionToast(t("transport.title"));

  const mutation = useTransportMutation(isEdit);
  useNavigateOnCriteria("/", mutation.isSuccess);

  return (
    <TransportFormContainer
      onSubmit={handleSubmit((data) => onSubmitForm(mapFormTransportLibToApiModel(data), mutation.mutateAsync, toast))}
    >
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <TransportFormBaseFields
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
    </TransportFormContainer>
  );
};
