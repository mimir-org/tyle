import { DevTool } from "@hookform/devtools";
import { UnitLibCm, UnitLibAm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { createEmptyUnit, toUnitLibAm } from "./types/formUnitLib";
import { useUnitMutation, useUnitQuery } from "./UnitForm.helpers";
import UnitFormBaseFields from "./UnitFormBaseFields";
import { AttributeFormContainer } from "../attributes/AttributeFormContainer.styled";

interface UnitFormProps {
  defaultValues?: UnitLibAm;
}

export const UnitForm = ({ defaultValues = createEmptyUnit() }: UnitFormProps) => {
  const { t } = useTranslation("entities");

  const formMethods = useForm<UnitLibAm>({
    defaultValues: defaultValues,
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useUnitQuery();
  const mapper = (source: UnitLibCm) => toUnitLibAm(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useUnitMutation();
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("unit.title"));

  return (
    <FormProvider {...formMethods}>
      <AttributeFormContainer
        onSubmit={handleSubmit((data) => {
          onSubmitForm(data, mutation.mutateAsync, toast);
        })}
      >
        {isLoading ? (
          <Loader />
        ) : (
          <>
            <UnitFormBaseFields />
            <DevTool control={control} placement={"bottom-right"} />
          </>
        )}
      </AttributeFormContainer>
    </FormProvider>
  );
};
