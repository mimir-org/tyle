import { DevTool } from "@hookform/devtools";
import { AttributeGroupLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useAttributeGroupMutation, useAttributeGroupQuery } from "./AttributeGroupForm.helpers";
import { AttributeGroupFormBaseFields } from "./AttributeGroupFormBaseFields";
import {
  FormAttributeGroupLib,
  createEmptyAttributeGroup,
  fromFormAttributeGroupLibToApiModel,
  toFormAttributeGroupLib,
} from "./types/formAttributeGroupLib";
import { FormMode } from "../types/formMode";
import { Box, Button, Flexbox, FormContainer, Text } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { yupResolver } from "@hookform/resolvers/yup";
import { attributeGroupSchema } from "./attributeGroupSchema";
import { PlainLink } from "features/common/plain-link";
import { AttributeGroupFormPreview } from "../entityPreviews/attributeGroup/AttributeGroupFormPreview";

interface AttributeGroupFormProps {
  defaultValues?: FormAttributeGroupLib;
  mode?: FormMode;
}

export const AttributeGroupForm = ({ defaultValues = createEmptyAttributeGroup(), mode }: AttributeGroupFormProps) => {
  const { t } = useTranslation("entities");
  const theme = useTheme();

  const formMethods = useForm<FormAttributeGroupLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(attributeGroupSchema(t)),
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useAttributeGroupQuery();
  const mapper = (source: AttributeGroupLibCm) => toFormAttributeGroupLib(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useAttributeGroupMutation(query.data?.id, mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("attributeGroup.title"));

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) =>
          onSubmitForm(fromFormAttributeGroupLibToApiModel(data), mutation.mutateAsync, toast),
        )}
      >
        {isLoading ? (
          <Loader />
        ) : (
          <Box display={"flex"} flex={2} flexDirection={"row"} gap={theme.mimirorg.spacing.multiple(6)}>
            <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
              <Text variant={"display-small"}>{t("attributeGroup.title")}</Text>
              <AttributeGroupFormBaseFields limited={mode === "edit"} />
              <Flexbox justifyContent={"center"} gap={theme.mimirorg.spacing.xl}>
                <PlainLink tabIndex={-1} to={"/"}>
                  <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
                    {t("common.cancel")}
                  </Button>
                </PlainLink>
                <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
              </Flexbox>
            </Flexbox>
            <AttributeGroupFormPreview control={control} />
          </Box>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
