import { Form, FormField } from "complib/form";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import {
  copySecret,
  createSecret,
  encodeFile,
  FormMimirorgCompany,
  mapCompanyCmToFormCompany,
  mapFormCompanyToCompanyAm,
  useUpdateToast,
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
import { useTheme } from "styled-components";
import { Flexbox } from "complib/layouts";
import { useRef, useState } from "react";
import { DocumentDuplicate, PaperClip } from "@styled-icons/heroicons-outline";
import { isAxiosError } from "axios";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";
import { RadioFilters } from "../common/radio-filters/RadioFilters";
import { FileItemComponent } from "complib/inputs/file/components/FileItemComponent";
import { FileInfo } from "complib/inputs/file/FileComponent";
import { useUpdateCompany } from "external/sources/company/company.queries";

export const UpdateCompanyForm = () => {
  const companies = useGetFilteredCompanies(MimirorgPermission.Manage);
  const companyOptions = companies.map((x) => ({ value: String(x.id), label: x.displayName })) as Option<string>[];
  const [selectedCompany, setSelectedCompany] = useState(companyOptions[0]?.value);
  const [secret, setSecret] = useState<string>("");
  const [updateSecret, setUpdateSecret] = useState(false);
  const [previewLogo, setPreviewLogo] = useState<FileInfo | null>(null);

  const theme = useTheme();
  const { t } = useTranslation("settings");

  const formMethods = useForm<FormMimirorgCompany>({
    defaultValues: {
      ...mapCompanyCmToFormCompany(companies.find((c) => c.id === Number(selectedCompany))),
      secret: secret,
    },
    resolver: yupResolver(companySchema(t)),
  });

  const { register, handleSubmit, control, setError, formState, reset, setValue, getValues } = formMethods;
  const { ref, ...fields } = register("logo");
  const fileInputRef = useRef<HTMLInputElement | null>(null);

  const userQuery = useGetCurrentUser();

  const mutation = useUpdateCompany(selectedCompany);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const submitToast = useUpdateToast();

  const onSubmit = async (data: FormMimirorgCompany) => {
    if (userQuery.isSuccess) {
      try {
        await onSubmitForm(
          mapFormCompanyToCompanyAm(data, userQuery.data?.id, secret),
          mutation.mutateAsync,
          submitToast
        );
      } catch (e) {
        if (isAxiosError(e) && e.response?.status === 400) {
          if (e.response?.data.Name) toast.error(t("company.toasts.companyNameError"));
          if (e.response?.data.Domain) toast.error(t("company.toasts.companyDomainError"));
        }
      }
    } else toast.error(t("company.toasts.userdataError"));
  };

  const onFileRemove = () => {
    setValue("logo", null);
    if (fileInputRef?.current?.value != null) fileInputRef.current.value = "";
    setPreviewLogo(null);
  };

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xxl}>
      <RadioFilters
        filters={companyOptions}
        value={selectedCompany}
        onChange={(x) => {
          setSelectedCompany(x);
          reset(mapCompanyCmToFormCompany(companies.find((c) => c.id === Number(x))));
          setUpdateSecret(false);
          setSecret("");
          getValues("logo") == null ? setPreviewLogo(null) : setPreviewLogo(getValues("logo"));
        }}
      />
      <FormProvider {...formMethods}>
        <Form onSubmit={handleSubmit((data) => onSubmit(data))}>
          <FormField label={t("company.labels.name")} error={formState.errors.name}>
            <Input placeholder={t("company.placeholders.name")} {...register("name")} />
          </FormField>
          <FormField label={t("company.labels.displayName")} error={formState.errors.displayName}>
            <Input placeholder={t("company.placeholders.displayName")} {...register("displayName")} />
          </FormField>
          <FormField label={t("company.labels.description")} error={formState.errors.description}>
            <Textarea placeholder={t("company.placeholders.description")} {...register("description")} />
          </FormField>
          {updateSecret ? (
            <FormField label={t("company.labels.secret")} error={formState.errors.secret}>
              <Input
                type="text"
                value={secret}
                readOnly
                icon={
                  <Button
                    icon={<DocumentDuplicate size={24} />}
                    onClick={() => copySecret(secret, t("company.toasts.copySecret"))}
                  >
                    {""}
                  </Button>
                }
              />
            </FormField>
          ) : (
            <FormField label={t("company.labels.hiddenSecret")}>
              <Button
                onClick={() => {
                  setSecret(createSecret(50));
                  setUpdateSecret(true);
                }}
              >
                Generate new secret
              </Button>
            </FormField>
          )}
          <FormField label={t("company.labels.domain")} error={formState.errors.domain}>
            <Input placeholder={t("company.placeholders.domain")} {...register("domain")} />
          </FormField>
          <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xs}>
            <FormField label={t("company.labels.logo")} error={formState.errors.logo}></FormField>
            <input
              accept=".svg,image/svg+xml"
              type={"file"}
              style={{ display: "none" }}
              {...fields}
              ref={(instance) => {
                ref(instance);
                fileInputRef.current = instance;
              }}
              onChange={async (e) => {
                if (e.currentTarget.files != null && e.currentTarget.files.length > 0) {
                  setValue("logo", await encodeFile(e.currentTarget.files[0]));
                  setPreviewLogo(getValues("logo"));
                } else {
                  setValue("logo", null);
                  setPreviewLogo(null);
                }
              }}
            />
            <Button icon={<PaperClip size={24} />} onClick={() => fileInputRef?.current?.click()}>
              Add attachment
            </Button>

            {previewLogo && (
              <div>
                <FileItemComponent fileInfo={previewLogo} onRemove={onFileRemove} />
              </div>
            )}
          </Flexbox>
          <FormField label={t("company.labels.homePage")} error={formState.errors.homePage}>
            <Input placeholder={t("company.placeholders.homePage")} {...register("homePage")} />
          </FormField>
          <Button type={"submit"}>{t("company.submit.update")}</Button>
          <DevTool control={control} placement={"bottom-right"} />
        </Form>
      </FormProvider>
    </Flexbox>
  );
};