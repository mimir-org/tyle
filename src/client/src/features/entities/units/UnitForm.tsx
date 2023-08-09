import { DevTool } from "@hookform/devtools";
import { UnitLibCm, UnitLibAm, State } from "@mimirorg/typelibrary-types";
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
import { UnitFormPreview } from "../entityPreviews/unit/UnitFormPreview";
import { FormContainer } from "../../../complib/form/FormContainer.styled";
import { FormMode } from "../types/formMode";
import { Box, Flexbox } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { yupResolver } from "@hookform/resolvers/yup";
import { unitSchema } from "./unitSchema";
import { PlainLink } from "features/common/plain-link";
import { Button } from "complib/buttons";
import { Text } from "../../../complib/text";

interface UnitFormProps {
  defaultValues?: UnitLibAm;
  mode?: FormMode;
}

export const UnitForm = ({ defaultValues = createEmptyUnit(), mode }: UnitFormProps) => {
  const { t } = useTranslation("entities");
  const theme = useTheme();

  const formMethods = useForm<UnitLibAm>({
    defaultValues: defaultValues,
    resolver: yupResolver(unitSchema(t)),
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useUnitQuery();
  const mapper = (source: UnitLibCm) => toUnitLibAm(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useUnitMutation(query.data?.id, mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("unit.title"));

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
              <Text variant={"display-small"}>{t("unit.title")}</Text>
              <UnitFormBaseFields limited={mode === "edit" && query.data?.state === State.Approved} />
              <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
                <PlainLink tabIndex={-1} to={"/"}>
                  <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
                    {t("common.cancel")}
                  </Button>
                </PlainLink>
                <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
              </Flexbox>
            </Flexbox>
            <UnitFormPreview control={control} />
          </Box>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
