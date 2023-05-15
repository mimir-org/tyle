import { DevTool } from "@hookform/devtools";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { QuantityDatumLibAm, QuantityDatumLibCm } from "@mimirorg/typelibrary-types";
import { createEmptyDatum, toDatumLibAm } from "./types/formQuantityDatumLib";
import { useQuantityDatumMutation, useQuantityDatumQuery } from "./QuantityDatumForm.helpers";
import { Flexbox } from "../../../complib/layouts";
import { PlainLink } from "../../common/plain-link";
import { Button } from "../../../complib/buttons";
import { useTheme } from "styled-components";
import { QuantityDatumFormBaseFields } from "./QuantityDatumFormBaseFields";
import QuantityDatumPreview from "../entityPreviews/quantityDatum/QuantityDatumPreview";
import { FormContainer } from "../../../complib/form/FormContainer.styled";

interface DatumFormProps {
  defaultValues?: QuantityDatumLibAm;
}

export const QuantityDatumForm = ({ defaultValues = createEmptyDatum() }: DatumFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<QuantityDatumLibAm>({
    defaultValues: defaultValues,
  });

  const { control, handleSubmit, setError, reset } = formMethods;

  const query = useQuantityDatumQuery();
  const mapper = (source: QuantityDatumLibCm) => toDatumLibAm(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useQuantityDatumMutation();
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("quantityDatum.title"));
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });
  const quantityDatumType = useWatch({ control, name: "quantityDatumType" });

  const quantityDatum = { name, description, quantityType: quantityDatumType };

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
          <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
            <QuantityDatumFormBaseFields />
            <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
              <PlainLink tabIndex={-1} to={"/"}>
                <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
                  {t("common.cancel")}
                </Button>
              </PlainLink>
              <Button type={"submit"}>{t("common.submit")}</Button>
            </Flexbox>
          </Flexbox>
        )}
        <QuantityDatumPreview {...quantityDatum} />
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
