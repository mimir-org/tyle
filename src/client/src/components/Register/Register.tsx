import Completion from "components/Completion";
import MultiFactorAuthentication from "components/MultiFactorAuthentication";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { QrCodeView } from "types/authentication/qrCodeView";
import RegisterDetails from "./RegisterDetails";
import RegisterVerification from "./RegisterVerification";

export type RegisterSteps = "DETAILS" | "VERIFY" | "MFA" | "COMPLETE";

const Register = () => {
  const [stage, setStage] = useState<RegisterSteps>("DETAILS");
  const [email, setEmail] = useState("");
  const [mfaInfo, setMfaInfo] = useState<QrCodeView>({ code: "", manualCode: "" });
  const navigate = useNavigate();

  return (
    <>
      {stage === "DETAILS" && (
        <RegisterDetails
          setUserEmail={setEmail}
          complete={{
            actionable: true,
            actionText: "Create account",
            onAction: () => setStage("VERIFY"),
          }}
        />
      )}
      {stage === "VERIFY" && (
        <RegisterVerification
          email={email}
          setMfaInfo={setMfaInfo}
          complete={{
            actionable: true,
            actionText: "Next",
            onAction: () => setStage("MFA"),
          }}
          cancel={{
            actionable: true,
            actionText: "Back",
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "MFA" && (
        <MultiFactorAuthentication
          mfaInfo={mfaInfo}
          title="Register authenticator"
          infoText="In order to use :Tyle, you must activate 2-factor authentication using an authenticator app. This is to ensure the quality fo the data created and stored in :Tyle."
          codeTitle="Scan the QR code"
          manualCodeTitle="Can't scan image?"
          manualCodeDescription="Copy this code into your authenticator app of choice"
          complete={{
            actionable: true,
            actionText: "Next",
            onAction: () => setStage("COMPLETE"),
          }}
          cancel={{
            actionable: true,
            actionText: "Back",
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "COMPLETE" && (
        <Completion
          title="Register"
          infoText="Account creation complete. Please return to the main page and log in."
          complete={{
            actionable: true,
            actionText: "Return",
            onAction: () => navigate("/"),
          }}
        />
      )}
    </>
  );
};

export default Register;
