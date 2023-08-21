import { StoryFn } from "@storybook/react";
import { PencilSquare } from "@styled-icons/heroicons-outline";
import { Button } from "@mimirorg/component-library";
import { UserListItem } from "features/settings/permission/user-list/UserListItem";

export default {
  title: "Features/Settings/Permission/UserListItem",
  component: UserListItem,
};

const Template: StoryFn<typeof UserListItem> = (args) => <UserListItem {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Jane Smith",
  trait: "Approve",
  action: (
    <Button variant={"text"} icon={<PencilSquare />} iconOnly>
      Edit
    </Button>
  ),
};
