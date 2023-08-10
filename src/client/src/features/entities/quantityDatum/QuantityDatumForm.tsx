import { DevTool } from "@hookform/devtools";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { QuantityDatumLibAm, QuantityDatumLibCm, State } from "@mimirorg/typelibrary-types";
import { createEmptyDatum, toDatumLibAm } from "./types/formQuantityDatumLib";
import { useQuantityDatumMutation, useQuantityDatumQuery } from "./QuantityDatumForm.helpers";
import { Flexbox, Box, Text } from "@mimirorg/component-library";
import { PlainLink } from "../../common/plain-link";
import { Button } from "../../../complib/buttons";
import { useTheme } from "styled-components";
import { QuantityDatumFormBaseFields } from "./QuantityDatumFormBaseFields";
import { FormContainer } from "../../../complib/form/FormContainer.styled";
import { FormMode } from "../types/formMode";
import { QuantityDatumFormPreview } from "../entityPreviews/quantityDatum/QuantityDatumFormPreview";
import { yupResolver } from "@hookform/resolvers/yup";
import { quantityDatumSchema } from "./quantityDatumSchema";

/**
 * Props for QuantityDatumForm
 * @interface QuantityDatumFormProps
 * @constructor
 * @type QuantityDatumLibAm
 * @param {QuantityDatumLibAm} defaultValues - default values for the form
 * @type FormMode
 * @param {FormMode} mode - form mode
 */
interface QuantityDatumFormProps {
  defaultValues?: QuantityDatumLibAm;
  mode?: FormMode;
}

/**
 * Form for creating and editing QuantityDatum entities
 * @interface QuantityDatumFormProps
 */
export const QuantityDatumForm = ({ defaultValues = createEmptyDatum(), mode }: QuantityDatumFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<QuantityDatumLibAm>({
    defaultValues: defaultValues,
    resolver: yupResolver(quantityDatumSchema(t)),
  });

  const { control, handleSubmit, setError, reset } = formMethods;

  const query = useQuantityDatumQuery();
  const mapper = (source: QuantityDatumLibCm) => toDatumLibAm(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useQuantityDatumMutation(query.data?.id, mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("quantityDatum.title"));

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) => {
          onSubmitForm(data, mutation.mutateAsync, toast);
        })}
      >
        {isLoading ? (
          <Loader />
        ) : (
          <Box display={"flex"} flex={2} flexDirection={"row"} gap={theme.tyle.spacing.multiple(6)}>
            <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
              <Text variant={"display-small"}>{t("quantityDatum.title")}</Text>
              <QuantityDatumFormBaseFields limited={mode === "edit" && query.data?.state === State.Approved} />
              <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
                <PlainLink tabIndex={-1} to={"/"}>
                  <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
                    {t("common.cancel")}
                  </Button>
                </PlainLink>
                <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
              </Flexbox>
            </Flexbox>
            <QuantityDatumFormPreview control={control} />
          </Box>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
