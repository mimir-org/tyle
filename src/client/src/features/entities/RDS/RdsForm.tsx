import { DevTool } from "@hookform/devtools";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { RdsLibAm, RdsLibCm, State } from "@mimirorg/typelibrary-types";
import { Box, Flexbox } from "../../../complib/layouts";
import { PlainLink } from "../../common/plain-link";
import { Button } from "../../../complib/buttons";
import { useTheme } from "styled-components";
import { createEmptyRds, toRdsLibAm } from "./types/formRdsLib";
import { useRdsMutation, useRdsQuery } from "./RdsForm.helpers";
import { RdsFormBaseFields } from "./RdsFormBaseFields";
import { RdsFormPreview } from "../entityPreviews/rds/RdsFormPreview";
import { FormContainer } from "../../../complib/form/FormContainer.styled";
import { FormMode } from "../types/formMode";
import { Text } from "../../../complib/text";
import { yupResolver } from "@hookform/resolvers/yup";
import { rdsSchema } from "./rdsSchema";

interface RdsFormProps {
  defaultValues?: RdsLibAm;
  mode?: FormMode;
}

export const RdsForm = ({ defaultValues = createEmptyRds(), mode }: RdsFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<RdsLibAm>({
    defaultValues: defaultValues,
    resolver: yupResolver(rdsSchema(t)),
  });

  const { control, handleSubmit, setError, reset } = formMethods;

  const query = useRdsQuery();
  const mapper = (source: RdsLibCm) => toRdsLibAm(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useRdsMutation(query.data?.id, mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("rds.title"));

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
              <Text variant={"display-small"}>{t("rds.title")}</Text>
              <RdsFormBaseFields limited={mode === "edit" && query.data?.state === State.Approved} />
              <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
                <PlainLink tabIndex={-1} to={"/"}>
                  <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
                    {t("common.cancel")}
                  </Button>
                </PlainLink>
                <Button type={"submit"}>{t("common.submit")}</Button>
              </Flexbox>
            </Flexbox>
            <RdsFormPreview control={control} />
          </Box>
        )}
      </FormContainer>
      <DevTool control={control} placement={"bottom-right"} />
    </FormProvider>
  );
};
