import { Form, FormField } from "complib/form";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import {
  copySecret,
  createEmptyFormMimirorgCompany,
  createSecret,
  FormMimirorgCompany,
  mapCompanyCmToFormCompany,
  mapFormCompanyToCompanyAm,
  useCompanyMutation,
  useCreatingToast,
} from "features/settings/company/CompanyForm.helpers";
import { yupResolver } from "@hookform/resolvers/yup";
import { companySchema } from "features/settings/company/companySchema";
import { useGetCurrentUser } from "external/sources/user/user.queries";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { Input, Textarea } from "complib/inputs";
import { toast } from "complib/data-display";
import { Button } from "complib/buttons";
import { DevTool } from "@hookform/devtools";
import { FileComponent } from "complib/inputs/file/FileComponent";
import { useTheme } from "styled-components";
import { Flexbox } from "complib/layouts";
import { useState } from "react";
import { DocumentDuplicate } from "@styled-icons/heroicons-outline";
import { isAxiosError } from "axios";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";
import { RadioFilters } from "../common/radio-filters/RadioFilters";

export const CompanyForm = () => {
  const companies = useGetFilteredCompanies(MimirorgPermission.Manage);
  const companyOptions = companies.map(x => ({ value: String(x.id), label: x.displayName })) as Option<string>[];
  companyOptions.unshift({ value: "0", label: "Create new company" });
  const [selectedCompany, setSelectedCompany] = useState(companyOptions[0]?.value);
  const [secret, setSecret] = useState<string>(createSecret(50));
  const [updateSecret, setUpdateSecret] = useState(true);
  
  const theme = useTheme();
  const { t } = useTranslation("settings");

  const formMethods = useForm<FormMimirorgCompany>({
    defaultValues: { ...createEmptyFormMimirorgCompany(), secret: secret },
    resolver: yupResolver(companySchema(t)),
  });

  const { register, handleSubmit, control, setError, formState, reset } = formMethods;

  const userQuery = useGetCurrentUser();

  const mutation = useCompanyMutation(selectedCompany);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const creationToast = useCreatingToast();

  const onSubmit = async (data: FormMimirorgCompany) => {
    if (userQuery.isSuccess) {
      try {
        await onSubmitForm(mapFormCompanyToCompanyAm(data, userQuery.data?.id, secret), mutation.mutateAsync, creationToast);
      } catch (e) {
        if (isAxiosError(e) && e.response?.status == 400) {
          if (e.response?.data.Name) toast.error(t("createCompany.toasts.companyNameError"));
          if (e.response?.data.Domain) toast.error(t("createCompany.toasts.companyDomainError"));
        }
      }
    } else toast.error(t("createCompany.toasts.userdataError"));
  };

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xxl}>
      <RadioFilters
        filters={companyOptions}
        value={selectedCompany}
        onChange={(x) => { 
          setSelectedCompany(x);
          reset(mapCompanyCmToFormCompany(companies.find(c => c.id == Number(x))));
          if (x == "0") {
            setSecret(createSecret(50));
            setUpdateSecret(true);
          } else {
            setUpdateSecret(false);
            setSecret("");
          }
        } }
      />
      <FormProvider {...formMethods}>
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
          { updateSecret ?
            <FormField label={t("createCompany.labels.secret")} error={formState.errors.secret}>
              <Input
                type="text"
                value={secret}
                readOnly
                icon={
                <Button
                    icon={<DocumentDuplicate size={24} />}
                    onClick={() => copySecret(secret, t("createCompany.toasts.copySecret"))}
                  >
                    {""}
                  </Button>
                }
                {...register("secret")}
              />
            </FormField> :
            <FormField label={t("createCompany.labels.hiddenSecret")}>
              <Button onClick={() => {
                setSecret(createSecret(50));
                setUpdateSecret(true);
              }}>Generate new secret</Button>
            </FormField>
          }
          <FormField label={t("createCompany.labels.domain")} error={formState.errors.domain}>
            <Input placeholder={t("createCompany.placeholders.domain")} {...register("domain")} />
          </FormField>
          <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xs}>
            <FormField label={t("createCompany.labels.logo")} error={formState.errors.logo}></FormField>
            <Controller
              control={control}
              name={"logo"}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <FileComponent {...rest} accept=".svg,image/svg+xml" ref={ref} value={value} onChange={onChange} />
              )}
            />
          </Flexbox>
          <FormField label={t("createCompany.labels.homePage")} error={formState.errors.homePage}>
            <Input placeholder={t("createCompany.placeholders.homePage")} {...register("homePage")} />
          </FormField>
          <Button type={"submit"}>{selectedCompany == "0" ? t("createCompany.submit.create") : t("createCompany.submit.update")}</Button>
          <DevTool control={control} placement={"bottom-right"} />
        </Form>
      </FormProvider>
    </Flexbox>
  );
};
