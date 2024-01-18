import { ArrowTopRightOnSquare } from "@styled-icons/heroicons-outline";
import UserMenuButton from "./UserMenuButton";

const FeedbackButton = () => {
  return (
    <UserMenuButton
      icon={<ArrowTopRightOnSquare size={24} />}
      onClick={() =>
        window.open("https://github.com/mimir-org/typelibrary/issues/new/choose", "_blank", "rel=noopener noreferrer")
      }
    >
      Provide feedback
    </UserMenuButton>
  );
};

export default FeedbackButton;
