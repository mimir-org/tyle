import { DevTool } from "@hookform/devtools";
import { AttributeLibCm, State } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useAttributeMutation, useAttributeQuery } from "./AttributeForm.helpers";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";
import {
  createEmptyAttributeTypeRequest,
  toAttributeTypeRequest,
} from "./types/formAttributeLib";
import { AttributeFormPreview } from "../entityPreviews/attribute/AttributeFormPreview";
import { FormMode } from "../types/formMode";
import { Box, Button, Flexbox, FormContainer, Text } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { yupResolver } from "@hookform/resolvers/yup";
import { attributeSchema } from "./attributeSchema";
import { PlainLink } from "features/common/plain-link";
import { AttributeTypeRequest } from "common/types/attributes/attributeTypeRequest";
import { AttributeView } from "common/types/attributes/attributeView";

interface AttributeFormProps {
  defaultValues?: AttributeTypeRequest;
  mode?: FormMode;
}

export const AttributeForm = ({ defaultValues = createEmptyAttributeTypeRequest(), mode }: AttributeFormProps) => {
  const { t } = useTranslation("entities");
  const theme = useTheme();

  const formMethods = useForm<AttributeTypeRequest>({
    defaultValues: defaultValues,
    resolver: yupResolver(attributeSchema(t)),
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useAttributeQuery();
  const mapper = (source: AttributeView) => toAttributeTypeRequest(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useAttributeMutation(query.data?.id, mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("attribute.title"));

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) =>
          onSubmitForm(data, mutation.mutateAsync, toast),
        )}
      >
        {isLoading ? (
          <Loader />
        ) : (
          <Box display={"flex"} flex={2} flexDirection={"row"} gap={theme.mimirorg.spacing.multiple(6)}>
            <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
              <Text variant={"display-small"}>{t("attribute.title")}</Text>
              <AttributeFormBaseFields limited={false} />
              <Flexbox justifyContent={"center"} gap={theme.mimirorg.spacing.xl}>
                <PlainLink tabIndex={-1} to={"/"}>
                  <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
                    {t("common.cancel")}
                  </Button>
                </PlainLink>
                <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
              </Flexbox>
            </Flexbox>
            {/*<AttributeFormPreview control={control} />*/}
          </Box>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
