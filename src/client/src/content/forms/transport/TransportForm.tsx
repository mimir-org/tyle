import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useCreateTransport, useUpdateTransport } from "../../../data/queries/tyle/queriesTransport";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { FormAttributes } from "../common/FormAttributes";
import { prepareAttributesByAspect } from "../common/prepareAttributesByAspect";
import { useSubmissionToast } from "../common/useSubmissionToast";
import { usePrefilledTransportData } from "./TransportForm.helpers";
import { TransportFormContainer } from "./TransportForm.styled";
import { TransportFormBaseFields } from "./TransportFormBaseFields";
import { createEmptyFormTransportLib, FormTransportLib, mapFormTransportLibToApiModel } from "./types/formTransportLib";

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

  const [hasPrefilledData, isLoading] = usePrefilledTransportData(reset);

  const createMutation = useCreateTransport();
  const updateMutation = useUpdateTransport();
  const targetMutation = isEdit ? updateMutation : createMutation;

  const toast = useSubmissionToast(t("transport.title"));
  const onSubmit = (data: FormTransportLib) => {
    const submittable = mapFormTransportLibToApiModel(data);
    const submissionPromise = targetMutation.mutateAsync(submittable);
    toast(submissionPromise);
    return submissionPromise;
  };

  useNavigateOnCriteria("/", targetMutation.isSuccess);

  return (
    <TransportFormContainer onSubmit={handleSubmit((data) => onSubmit(data))}>
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <TransportFormBaseFields
            control={control}
            register={register}
            resetField={resetField}
            setValue={setValue}
            hasPrefilledData={hasPrefilledData}
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
