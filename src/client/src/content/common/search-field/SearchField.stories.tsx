import { ComponentStory } from "@storybook/react";
import { SearchField } from "./SearchField";

export default {
  title: "Content/Common/SearchField",
  component: SearchField,
};

const Template: ComponentStory<typeof SearchField> = (args) => <SearchField {...args} />;

export const Default = Template.bind({});
