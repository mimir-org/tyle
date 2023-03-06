import { Form, FormField } from "complib/form";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import {
  createEmptyFormMimirorgCompany,
  FormMimirorgCompany,
  mapFormCompanyToCompanyAm,
  useCreatingToast,
} from "features/settings/company/CreateCompanyForm.helpers";
import { yupResolver } from "@hookform/resolvers/yup";
import { companySchema } from "features/settings/company/companySchema";
import { useGetCurrentUser } from "external/sources/user/user.queries";
import { useCreateCompany } from "external/sources/company/company.queries";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { Input, Textarea } from "complib/inputs";
import { toast } from "complib/data-display";
import { Button } from "complib/buttons";
import { DevTool } from "@hookform/devtools";
import { FileComponent } from "complib/inputs/file/FileComponent";

export const CreateCompanyForm = () => {
  const { t } = useTranslation("settings");

  const formMethods = useForm<FormMimirorgCompany>({
    defaultValues: createEmptyFormMimirorgCompany(),
    resolver: yupResolver(companySchema(t)),
  });

  const { register, handleSubmit, control, setError, formState } = formMethods;

  const userQuery = useGetCurrentUser();
  
  const mutation = useCreateCompany();
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const creationToast = useCreatingToast();

  const onSubmit = (data: FormMimirorgCompany) => {
    if (userQuery.isSuccess)
        onSubmitForm(mapFormCompanyToCompanyAm(data, userQuery.data?.id), mutation.mutateAsync, creationToast);
    else
        toast.error("Could not fetch user data, please try again.");
  }

  return (
    <Form onSubmit={handleSubmit((data) => onSubmit(data))}>
        <FormField label={t("createCompany.labels.name")} error={formState.errors.name}>
            <Input placeholder={t("createCompany.placeholders.name")} {...register("name")} />
        </FormField>
        <FormField label={t("createCompany.labels.displayName")} error={formState.errors.displayName}>
            <Input placeholder={t("createCompany.placeholders.displayName")} {...register("displayName")} />
        </FormField>
        <FormField label={t("createCompany.labels.description")} error={formState.errors.description}>
            <Textarea placeholder={t("createCompany.placeholders.description")} {...register("description")} />
        </FormField>
        <FormField label={t("createCompany.labels.domain")} error={formState.errors.domain}>
            <Input placeholder={t("createCompany.placeholders.domain")} {...register("domain")} />
        </FormField>
        <FormField label={t("createCompany.labels.logo")} error={formState.errors.logo}>
            <FileComponent />
        </FormField>
        <FormField label={t("createCompany.labels.homePage")} error={formState.errors.homePage}>
            <Input placeholder={t("createCompany.placeholders.homePage")} {...register("homePage")} />
        </FormField>
        <Button type={"submit"}>{t("createCompany.submit")}</Button>
        <DevTool control={control} placement={"bottom-right"} />
    </Form>
  );
};
