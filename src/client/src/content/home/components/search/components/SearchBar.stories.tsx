import { ComponentMeta, ComponentStory } from "@storybook/react";
import { SearchBar } from "./SearchBar";

export default {
  title: "Content/Home/Search/SearchBar",
  component: SearchBar,
  args: {
    setSearchQuery: (value) => console.log(`[STORYBOOK] SearchBar.setSearchQuery: ${value}`),
  },
} as ComponentMeta<typeof SearchBar>;

const Template: ComponentStory<typeof SearchBar> = (args) => <SearchBar {...args} />;

export const Default = Template.bind({});
