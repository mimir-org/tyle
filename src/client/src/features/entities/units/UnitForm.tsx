import { DevTool } from "@hookform/devtools";
import { AttributeLibAm, AttributeLibCm, UnitLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { createEmptyUnit, toUnitLibAm } from "./types/formUnitLib";
import { UnitLibAm } from "@mimirorg/typelibrary-types/index";
import { useUnitMutation, useUnitQuery } from "./UnitForm.helpers";

interface UnitFormProps {
  defaultValues?: UnitLibAm;
}

export const AttributeForm = ({ defaultValues = createEmptyUnit() }: UnitFormProps) => {
  const { t } = useTranslation("entities");

  const formMethods = useForm<AttributeLibAm>({
    defaultValues: defaultValues,
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useUnitQuery();
  const mapper = (source: UnitLibCm) => toUnitLibAm(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useUnitMutation();
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("attribute.title"));

  return (
    <FormProvider {...formMethods}>
      <div
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
      </div>
    </FormProvider>
  );
};
