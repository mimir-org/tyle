import { DevTool } from "@hookform/devtools";
import { MimirorgQrCodeCm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { useExecuteOnCriteria } from "common/hooks/useExecuteOnCriteria";
import { Button } from "complib/buttons";
import { Digits, Input } from "complib/inputs";
import { Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { Actionable } from "complib/types";
import { useGenerateMfa } from "external/sources/user/user.queries";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { AuthContent } from "../../common/auth-content/AuthContent";
import { Error } from "../../common/error/Error";
import { Processing } from "../../common/processing/Processing";
import { MotionVerifyForm } from "../../common/verification/Verification";
import { onSubmitForm } from "./RecoverVerification.helpers";

type VerificationProps = Pick<MimirorgVerifyAm, "email"> & {
  setMfaInfo: (info: MimirorgQrCodeCm) => void;
  setVerificationInfo?: (info: MimirorgVerifyAm) => void;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

export const RecoverVerification = ({
  email,
  setMfaInfo,
  setVerificationInfo,
  cancel,
  complete,
}: VerificationProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { control, register, handleSubmit } = useForm<MimirorgVerifyAm>();

  const generateMfaMutation = useGenerateMfa();

  const showProcessing = generateMfaMutation.isLoading;
  const showError = generateMfaMutation.isError;
  const showInput = !showProcessing;

  useExecuteOnCriteria(complete?.onAction, generateMfaMutation.isSuccess);

  return (
    <AuthContent
      title={t("recover.verify.title")}
      firstRow={
        <>
          {showProcessing && <Processing>{t("recover.processing")}</Processing>}
          {showError && <Error>{t("recover.verify.error")}</Error>}
          {showInput && (
            <MotionVerifyForm
              id={"verify-form"}
              onSubmit={handleSubmit((data) =>
                onSubmitForm(data, generateMfaMutation.mutateAsync, setMfaInfo, setVerificationInfo)
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
          <Text textAlign={"center"}>{t("recover.verify.info.text")}</Text>
          <Flexbox gap={theme.tyle.spacing.xxl} alignSelf={"center"}>
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
