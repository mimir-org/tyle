import { MimirorgQrCodeCm } from "@mimirorg/typelibrary-types";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { RegisterComplete } from "./components/RegisterComplete";
import { RegisterDetails } from "./components/RegisterDetails";
import { RegisterMfa } from "./components/RegisterMfa";
import { RegisterVerify } from "./components/RegisterVerify";

export type RegisterSteps = "DETAILS" | "VERIFY" | "MFA" | "COMPLETE";

export const Register = () => {
  const { t } = useTranslation();
  const [stage, setStage] = useState<RegisterSteps>("DETAILS");
  const [userEmail, setUserEmail] = useState("");
  const [qrCodeInfo, setQrCodeInfo] = useState<MimirorgQrCodeCm>({ code: "", manualCode: "" });
  const navigate = useNavigate();

  return (
    <>
      {stage === "DETAILS" && (
        <RegisterDetails
          setUserEmail={setUserEmail}
          complete={{
            actionable: true,
            actionText: t("register.details.submit"),
            onAction: () => setStage("VERIFY"),
          }}
        />
      )}
      {stage === "VERIFY" && (
        <RegisterVerify
          email={userEmail}
          setQrCodeInfo={setQrCodeInfo}
          complete={{
            actionable: true,
            actionText: t("common.next"),
            onAction: () => setStage("MFA"),
          }}
          cancel={{
            actionable: true,
            actionText: t("common.back"),
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "MFA" && (
        <RegisterMfa
          title={t("register.mfa.title")}
          infoText={t("register.mfa.info.text")}
          codeTitle={t("register.mfa.code.title")}
          manualCodeTitle={t("register.mfa.manual.title")}
          manualCodeDescription={t("register.mfa.manual.description")}
          code={qrCodeInfo.code}
          manualCode={qrCodeInfo.manualCode}
          complete={{
            actionable: true,
            actionText: t("common.next"),
            onAction: () => setStage("COMPLETE"),
          }}
          cancel={{
            actionable: true,
            actionText: t("common.back"),
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "COMPLETE" && (
        <RegisterComplete
          title={t("register.complete.title")}
          infoText={t("register.complete.info.text")}
          actionable
          actionText={t("common.return")}
          onAction={() => navigate("/")}
        />
      )}
    </>
  );
};
