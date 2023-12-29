import { BookOpen } from "@styled-icons/heroicons-outline";
import UserMenuButton from "./UserMenuButton";

const DocumentationButton = () => {
  return (
    <UserMenuButton
      icon={<BookOpen size={24} />}
      onClick={() => window.open("https://mimir-org.github.io/documents/", "_blank", "rel=noopener noreferrer")}
    >
      Documentation
    </UserMenuButton>
  );
};

export default DocumentationButton;