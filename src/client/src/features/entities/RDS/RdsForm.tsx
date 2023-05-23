import { DevTool } from "@hookform/devtools";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { RdsLibCm } from "@mimirorg/typelibrary-types";
import { AttributeFormContainer } from "../attributes/AttributeFormContainer.styled";
import { Flexbox } from "../../../complib/layouts";
import { PlainLink } from "../../common/plain-link";
import { Button } from "../../../complib/buttons";
import { useTheme } from "styled-components";
import { createEmptyRds, toRdsLibAm } from "./types/formRdsLib";
import { useRdsMutation, useRdsQuery } from "./RdsForm.helpers";
import { RdsFormBaseFields } from "./RdsFormBaseFields";

interface RdsFormProps {
  defaultValues?: RdsLibCm;
}

export const RdsForm = ({ defaultValues = createEmptyRds() }: RdsFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<RdsLibCm>({
    defaultValues: defaultValues,
  });

  const { control, handleSubmit, setError, reset } = formMethods;

  const query = useRdsQuery();
  const mapper = (source: RdsLibCm) => toRdsLibAm(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useRdsMutation();
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("rds.title"));

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
          <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
            <RdsFormBaseFields />
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
      </AttributeFormContainer>
      <DevTool control={control} placement={"bottom-right"} />
    </FormProvider>
  );
};
