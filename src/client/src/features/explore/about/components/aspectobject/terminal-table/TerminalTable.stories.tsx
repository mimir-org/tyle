import { Meta, StoryFn } from "@storybook/react";
import { mockAspectObjectTerminalItem } from "common/utils/mocks";
import { TerminalTable } from "features/explore/about/components/aspectobject/terminal-table/TerminalTable";

export default {
  title: "Features/Common/Terminal/TerminalTable",
  component: TerminalTable,
  args: {
    terminals: [...Array(7)].map((_) => mockAspectObjectTerminalItem()),
  },
} as Meta<typeof TerminalTable>;

const Template: StoryFn<typeof TerminalTable> = (args) => <TerminalTable {...args} />;

export const Default = Template.bind({});
