import { DevTool } from "@hookform/devtools";
import { MimirorgQrCodeCm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { useExecuteOnCriteria } from "common/hooks/useExecuteOnCriteria";
import { Actionable, Button, Digits, Flexbox, Input, Text } from "@mimirorg/component-library";
import { useGenerateMfa, useVerification } from "external/sources/user/user.queries";
import { AuthContent } from "features/auth/common/auth-content/AuthContent";
import { Error } from "features/auth/common/error/Error";
import { Processing } from "features/auth/common/processing/Processing";
import { MotionVerifyForm } from "features/auth/common/verification/Verification";
import { onSubmitForm } from "features/auth/register/verification/RegisterVerification.helpers";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

type VerificationProps = Pick<MimirorgVerifyAm, "email"> & {
  setMfaInfo: (info: MimirorgQrCodeCm) => void;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

export const RegisterVerification = ({ email, setMfaInfo, cancel, complete }: VerificationProps) => {
  const theme = useTheme();
  const { t } = useTranslation("auth");
  const { control, register, handleSubmit } = useForm<MimirorgVerifyAm>();

  const generateMfaMutation = useGenerateMfa();
  const verificationMutation = useVerification();

  const showError = generateMfaMutation.isError || verificationMutation.isError;
  const showProcessing = verificationMutation.isLoading || generateMfaMutation.isLoading;
  const showInput = !verificationMutation.isSuccess && !verificationMutation.isLoading;

  useExecuteOnCriteria(complete?.onAction, verificationMutation.isSuccess && generateMfaMutation.isSuccess);

  return (
    <AuthContent
      title={t("register.verify.title")}
      firstRow={
        <>
          {showProcessing && <Processing>{t("register.processing")}</Processing>}
          {showError && <Error>{t("register.verify.error")}</Error>}
          {showInput && (
            <MotionVerifyForm
              id={"verify-form"}
              onSubmit={handleSubmit((data) =>
                onSubmitForm(data, verificationMutation.mutateAsync, generateMfaMutation.mutateAsync, setMfaInfo),
              )}
              layout
            >
              <Input type={"hidden"} value={email} {...register("email")} />
              <Controller
                control={control}
                name={"code"}
                render={({ field: { value, onChange } }) => <Digits value={value} onChange={onChange} />}
              />
            </MotionVerifyForm>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("register.verify.info.text")}</Text>
          <Flexbox gap={theme.mimirorg.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && (
              <Button type={"submit"} form={"verify-form"}>
                {complete.actionText}
              </Button>
            )}
          </Flexbox>
        </>
      }
    />
  );
};
